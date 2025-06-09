using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Roles;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class RolService : IRolService
{
    private readonly IRolRepository _rolRepository;

    public RolService(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<IEnumerable<RolMinResponse>>> ObtenerRoles()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var roles = await _rolRepository.ObtenerRoles();
        
        scope.Complete();

        return Result<IEnumerable<RolMinResponse>>.Ok(roles);
    }

    public async Task<Result<RolResponse>> ObtenerRol(int codigoRol)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var rol = await _rolRepository.ObtenerRol(codigoRol);

        if (rol is null)
        {
            return Result<RolResponse>.Fail("Rol no encontrado");
        }

        var permisos = await _rolRepository.ObtenerPermisosRol(codigoRol);
        
        rol.Permisos = permisos.ToList();
        
        scope.Complete();
        return Result<RolResponse>.Ok(rol);
    }

    public async Task<Result<IEnumerable<PermisoResponse>>> ObtenerPermisos()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var permisos = await _rolRepository.ObtenerPermisos();

        scope.Complete();
        return Result<IEnumerable<PermisoResponse>>.Ok(permisos);
    }
}