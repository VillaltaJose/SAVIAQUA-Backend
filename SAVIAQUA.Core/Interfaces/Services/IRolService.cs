using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Roles;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IRolService
{
    Task<Result<IEnumerable<RolMinResponse>>> ObtenerRoles();
    Task<Result<RolResponse>> ObtenerRol(int codigoRol);
    Task<Result<IEnumerable<PermisoResponse>>> ObtenerPermisos();
    Task<Result<int>> RegistrarRol(CrearRolRequest request);
    Task<Result> ActualizarRol(EditarRolRequest request);
}