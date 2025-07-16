using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SAVIAQUA.API.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _connectedUsers = new();
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.Identity?.Name; // O usa Claim específico
        var connectionId = Context.ConnectionId;

        if (userId != null)
        {
            _connectedUsers[connectionId] = userId;
            Console.WriteLine($"Usuario {userId} conectado con ID {connectionId}");
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connectedUsers.TryRemove(Context.ConnectionId, out var userId);
        Console.WriteLine($"Usuario {userId} desconectado.");

        await base.OnDisconnectedAsync(exception);
    }

    // Ejemplo de método protegido
    public Task SendMessage(string message)
    {
        var userId = Context.User?.Identity?.Name;
        return Clients.All.SendAsync("ReceiveMessage", $"{userId}: {message}");
    }
}