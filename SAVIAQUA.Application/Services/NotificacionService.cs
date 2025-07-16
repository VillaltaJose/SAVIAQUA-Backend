using SAVIAQUA.Core.App;
using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Notificaciones;
using SAVIAQUA.Core.Filters.General;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class NotificacionService : INotificacionService
{
    private readonly INotificacionRepository _notificacionRepository;
    private readonly Session _session;

    public NotificacionService(INotificacionRepository notificacionRepository, Session session)
    {
        _notificacionRepository = notificacionRepository;
        _session = session;
    }

    public async Task<Result<int>> ObtenerTotalNoLeidas()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var total = await _notificacionRepository.ObtenerTotalNoLeidas(_session.CodigoUsuario);
        
        scope.Complete();
        return Result<int>.Ok(total);
    }
    
    public async Task<Result> MarcarLeida(int codigoNotificacion)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        await _notificacionRepository.MarcarLeida(codigoNotificacion, _session.CodigoUsuario);
        
        scope.Complete();
        return Result.Ok();
    }

    public async Task<Result<IEnumerable<NotificacionResponse>>> ObtenerNotificaciones(PaginatedFilter filter)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        if (filter.PageSize > 20) filter.PageSize = 20;
        if (filter.PageNumber < 1) filter.PageNumber = 1;

        var total = await _notificacionRepository.ObtenerTotalNotificaciones(_session.CodigoUsuario);
        var notificaciones =
            await _notificacionRepository.ObtenerNotificaciones(_session.CodigoUsuario, filter.PageSize,
                filter.PageNumber);
        
        scope.Complete();

        var result = Result<IEnumerable<NotificacionResponse>>.Ok(notificaciones);

        result.Metadata = new Metadata
        {
            PageSize = filter.PageSize,
            CurrentPage = filter.PageNumber,
            TotalCount = total,
        };

        return result;
    }
}