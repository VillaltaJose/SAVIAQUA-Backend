using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Notificaciones;
using SAVIAQUA.Core.Filters.General;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface INotificacionService
{
    Task<Result<int>> ObtenerTotalNoLeidas();
    Task<Result> MarcarLeida(int codigoNotificacion);
    Task<Result<IEnumerable<NotificacionResponse>>> ObtenerNotificaciones(PaginatedFilter filter);
}