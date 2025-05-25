using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Roles;
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

    public async Task<IEnumerable<RolResponse>> ObtenerRoles()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var roles = await _dbConnection.QueryAsync<RolResponse>(RolesQueries.ObtenerRoles);
        scope.Complete();

        return roles;
    }
}