using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Notificaciones;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class NotificacionRepository : INotificacionRepository
{
    private readonly IDbConnection _dbConnection;

    public NotificacionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> ObtenerTotalNotificaciones(int codigoUsuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var total = await _dbConnection.QueryFirstAsync<int>(NotificacionesQueries.CountNotificaciones, new
        {
            codigoUsuario,
        });
        
        scope.Complete();
        return total;
    }

    public async Task<IEnumerable<NotificacionResponse>> ObtenerNotificaciones(int codigoUsuario, int pageSize, int pageNumber)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var notificaciones = await _dbConnection.QueryAsync<NotificacionResponse>(NotificacionesQueries.ObtenerNotificaciones, new
        {
            codigoUsuario,
            limit = pageSize,
            offset = (pageNumber - 1) * pageSize
        });
        
        scope.Complete();
        return notificaciones;
    }

    public async Task<int> ObtenerTotalNoLeidas(int codigoUsuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var total = await _dbConnection.QueryFirstAsync<int>(NotificacionesQueries.TotalNoLeidas, new
        {
            codigoUsuario,
        });
        
        scope.Complete();
        return total;
    }

    public async Task MarcarLeida(int codigoNotificacion, int codigoUsuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        await _dbConnection.ExecuteAsync(NotificacionesQueries.MarcarLeida, new
        {
            codigoUsuario,
            codigoNotificacion,
        });
        
        scope.Complete();
    }
}