using SAVIAQUA.Core.DTOs.Roles;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IRolRepository
{
    Task<IEnumerable<RolMinResponse>> ObtenerRoles();
    Task<RolResponse?> ObtenerRol(int codigoRol);
    Task<IEnumerable<int>> ObtenerPermisosRol(int codigoRol);
    Task<IEnumerable<PermisoResponse>> ObtenerPermisos();
}