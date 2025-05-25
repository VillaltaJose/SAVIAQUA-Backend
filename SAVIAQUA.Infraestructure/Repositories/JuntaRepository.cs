using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class JuntaRepository : IJuntaRepository
{
    private readonly IDbConnection _dbConnection;

    public JuntaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<JuntaMinResponse>> ObtenerJuntasMin()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var juntas = await _dbConnection.QueryAsync<JuntaMinResponse>(JuntasQueries.ObtenerJuntasMin);
        
        scope.Complete();
        return juntas;
    }
}