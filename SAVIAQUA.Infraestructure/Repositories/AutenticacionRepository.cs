using System.Data;
using Dapper;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class AutenticacionRepository : IAutenticacionRepository
{
    private readonly IDbConnection _dbConnection;

    public AutenticacionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Usuario?> ObtenerUsuarioPorCorreo(string correo)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var usuario = await _dbConnection.QueryFirstOrDefaultAsync<Usuario?>(AutenticacionQueries.ObtenerUsuarioPorCorreo, new { correo });
        
        scope.Complete();
        return usuario;
    }
}