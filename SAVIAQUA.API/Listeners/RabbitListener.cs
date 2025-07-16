using System.Data;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SAVIAQUA.API.Hubs;
using SAVIAQUA.Core.DTOs.Notificaciones;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Repositories;

namespace SAVIAQUA.API.Listeners;

public class RabbitListener : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitListener> _logger;
    private readonly IHubContext<NotificationHub> _hubContext;

    public RabbitListener(IServiceProvider serviceProvider, IHubContext<NotificationHub> hubContext, ILogger<RabbitListener> logger)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = "34.132.169.162",
                UserName = "kalo",
                Password = "kalo"
            };
            await using var connection = await factory.CreateConnectionAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(queue: "notificaciones",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var rawMessage = Encoding.UTF8.GetString(body);

                    _logger.LogInformation("Message received: {Message}", rawMessage);

                    var message = JsonSerializer.Deserialize<EventBusMessageRequest>(rawMessage);

                    if (message is null) return;
                    
                    using var scope = _serviceProvider.CreateScope();
                    var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
                    IAlertaRepository alertaRepository = new AlertaRepository(dbConnection);

                    var categoria = await alertaRepository.ObtenerDatosCategoria(message.CodigoCategoria);

                    if (categoria is null) return;

                    var parametros = await alertaRepository.ObtenerParametros(categoria.SqlParametros,
                        message.ReferenciaInt, message.ReferenciaStr);

                    var msg = categoria.Plantilla.Replace("{referenciaInt}", message.ReferenciaInt?.ToString());
                    msg = msg.Replace("{referenciaStr}", message.ReferenciaStr);

                    if (parametros is not null)
                    {
                        msg = parametros.Keys.Aggregate(msg, (current, key) => current.Replace("{" + key + "}", parametros[key].ToString()));
                    }
                    
                    if (message.ExtraParams is not null)
                    {
                        msg = message.ExtraParams.Keys.Aggregate(msg, (current, key) => current.Replace("{" + key + "}", message.ExtraParams[key].ToString()));
                    }

                    var usuarios = await alertaRepository.ObtenerDestinatarios(categoria.SqlDestinatarios,
                        message.ReferenciaInt, message.ReferenciaStr);
                    
                    foreach (var notificacion in usuarios.Select(usuario => new Notificacion
                             {
                                 CodigoUsuario = usuario,
                                 CodigoCategoria = message.CodigoCategoria,
                                 ReferenciaInt = message.ReferenciaInt,
                                 ReferenciaStr = message.ReferenciaStr,
                                 Texto = msg
                             }))
                    {
                        await alertaRepository.InsertarNotificacion(notificacion);
                    }

                    // Firebase
                    
                    // SignalR
                    // await _hubContext.Clients.All.SendAsync("RecibirNotificacion", message);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error procesando mensaje: {Error}", ex);
                }
            };

            await channel.BasicConsumeAsync(queue: "notificaciones",
                autoAck: true,
                consumer: consumer, cancellationToken: stoppingToken);
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en listener de rabbit");
            Console.WriteLine(ex);
        }
    }
}