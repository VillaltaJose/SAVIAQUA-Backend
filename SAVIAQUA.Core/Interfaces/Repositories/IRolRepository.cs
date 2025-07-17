using SAVIAQUA.Core.DTOs.Roles;
using SAVIAQUA.Core.Entities;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IRolRepository
{
    Task<IEnumerable<RolMinResponse>> ObtenerRoles();
    Task<RolResponse?> ObtenerRol(int codigoRol);
    Task<IEnumerable<int>> ObtenerPermisosRol(int codigoRol);
    Task<IEnumerable<PermisoResponse>> ObtenerPermisos();
    Task RegistrarPermiso(int codigoRol, int codigoPermiso);
    Task EliminarPermisosRol(int codigoRol);
    Task ActualizarRol(Rol rol);
    Task<int> CrearRol(Rol rol);
}