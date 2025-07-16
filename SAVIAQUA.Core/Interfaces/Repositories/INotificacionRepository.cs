using SAVIAQUA.Core.DTOs.Notificaciones;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface INotificacionRepository
{
    Task<int> ObtenerTotalNotificaciones(int codigoUsuario);

    Task<IEnumerable<NotificacionResponse>> ObtenerNotificaciones(int codigoUsuario, int pageSize, int pageNumber);

    Task<int> ObtenerTotalNoLeidas(int codigoUsuario);

    Task MarcarLeida(int codigoNotificacion, int codigoUsuario);
}