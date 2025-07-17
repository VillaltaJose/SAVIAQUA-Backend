using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Roles;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class RolRepository : IRolRepository
{
    private readonly IDbConnection _dbConnection;

    public RolRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<RolMinResponse>> ObtenerRoles()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var roles = await _dbConnection.QueryAsync<RolMinResponse>(RolesQueries.ObtenerRoles);
        
        scope.Complete();
        return roles;
    }

    public async Task<RolResponse?> ObtenerRol(int codigoRol)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var roles = await _dbConnection.QueryFirstOrDefaultAsync<RolResponse>(RolesQueries.ObtenerRol, new
        {
            codigoRol
        });
        
        scope.Complete();
        return roles;
    }

    public async Task<IEnumerable<int>> ObtenerPermisosRol(int codigoRol)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var permisos = await _dbConnection.QueryAsync<int>(RolesQueries.ObtenerPermisosRol, new
        {
            codigoRol
        });
        
        scope.Complete();
        return permisos;
    }

    public async Task<IEnumerable<PermisoResponse>> ObtenerPermisos()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var permisos = await _dbConnection.QueryAsync<PermisoResponse>(RolesQueries.ObtenerPermisos);
        
        scope.Complete();
        return permisos;
    }

    public async Task RegistrarPermiso(int codigoRol, int codigoPermiso)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        await _dbConnection.ExecuteAsync(RolesQueries.RegistrarPermiso, new
        {
            codigoRol,
            codigoPermiso,
        });
        
        scope.Complete();
    }
    
    public async Task EliminarPermisosRol(int codigoRol)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        await _dbConnection.ExecuteAsync(RolesQueries.EliminarPermisosRol, new
        {
            codigoRol,
        });
        
        scope.Complete();
    }

    public async Task ActualizarRol(Rol rol)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        await _dbConnection.ExecuteAsync(RolesQueries.ActualizarRol, rol);
        
        scope.Complete();
    }
    
    public async Task<int> CrearRol(Rol rol)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var codigo = await _dbConnection.ExecuteScalarAsync<int>(RolesQueries.CrearRol, rol);
        
        scope.Complete();
        return codigo;
    }
}