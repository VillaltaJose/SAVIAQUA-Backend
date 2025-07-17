using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Roles;
using SAVIAQUA.Core.Entities;
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

    public async Task<Result> ActualizarRol(EditarRolRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        await _rolRepository.EliminarPermisosRol(request.Codigo);
        
        foreach (var codigoPermiso in request.Permisos)
        {
            await _rolRepository.RegistrarPermiso(request.Codigo, codigoPermiso);
        }

        await _rolRepository.ActualizarRol(new Rol
        {
            Codigo = request.Codigo,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Activo = request.Activo
        });
        
        scope.Complete();
        return Result.Ok();
    }
    
    public async Task<Result<int>> RegistrarRol(CrearRolRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var codigo = await _rolRepository.CrearRol(new Rol
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Activo = request.Activo
        });
        
        Console.WriteLine(codigo);
        
        foreach (var codigoPermiso in request.Permisos)
        {
            await _rolRepository.RegistrarPermiso(codigo, codigoPermiso);
        }
        
        scope.Complete();
        return Result<int>.Ok(codigo);
    }
}