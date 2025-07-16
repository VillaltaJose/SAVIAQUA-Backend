using System.Data;
using System.Dynamic;
using Dapper;
using SAVIAQUA.Core.DTOs.Notificaciones;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class AlertaRepository : IAlertaRepository
{
    private readonly IDbConnection _dbConnection;

    public AlertaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task InsertarNotificacion(Notificacion notificacion)
    {
        await _dbConnection.ExecuteAsync(NotificacionesQueries.InsertarNotificacion, notificacion);
    }
    
    public async Task<CategoriaNotificacionResponse?> ObtenerDatosCategoria(int codigoCategoria)
    {
        var categoria = await _dbConnection.QueryFirstAsync<CategoriaNotificacionResponse?>(NotificacionesQueries.ObtenerCategoria, new
        {
            codigoCategoria
        });

        return categoria;
    }

    public async Task<Dictionary<string, object>?> ObtenerParametros(string sql, int? referenciaInt = null, string? referenciaStr = null)
    {
        var result = await _dbConnection.QueryFirstOrDefaultAsync(sql, new
        {
            referenciaInt,
            referenciaStr
        });
        
        if (result is null) return null;

        return ((IDictionary<string, object>)result).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    public async Task<List<int>> ObtenerDestinatarios(string sql, int? referenciaInt = null, string? referenciaStr = null)
    {
        var usuarios = await _dbConnection.QueryAsync<int>(sql, new
        {
            referenciaInt,
            referenciaStr
        });

        return usuarios.ToList();
    }
}