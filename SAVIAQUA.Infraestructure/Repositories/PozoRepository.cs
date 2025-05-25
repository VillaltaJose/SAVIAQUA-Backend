using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Pozos;
using SAVIAQUA.Core.Filters.Pozos;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class PozoRepository : IPozoRepository
{
    private readonly IDbConnection _dbConnection;

    public PozoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<(int, IEnumerable<PozoMinResponse>)> ObtenerPozos(ObtenerPozosFilter filter)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var pozos = await _dbConnection.QueryAsync<PozoMinResponse>(PozosQueries.ObtenerPozos, filter);

        var count = await _dbConnection.QueryFirstAsync<int>(PozosQueries.ObtenerTotalPozos, filter);
        
        scope.Complete();
        return (count, pozos);
    }
}