using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Lugares;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class LugarRepository : ILugarRepository
{
    private readonly IDbConnection _dbConnection;

    public LugarRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ProvinciaResponse>> ObtenerProvincias()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var data = await _dbConnection.QueryAsync<ProvinciaResponse>(LugaresQueries.ObtenerProvincias);
        
        scope.Complete();
        return data;
    }
    
    public async Task<IEnumerable<CiudadResponse>> ObtenerCiudades(int codigoProvincia)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var data = await _dbConnection.QueryAsync<CiudadResponse>(LugaresQueries.ObtenerCiudades, new
        {
            codigoProvincia
        });
        
        scope.Complete();
        return data;
    }
    
    public async Task<IEnumerable<ParroquiaResponse>> ObtenerParroquias(int codigoProvincia, int codigoCiudad)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var data = await _dbConnection.QueryAsync<ParroquiaResponse>(LugaresQueries.ObtenerParroquias, new
        {
            codigoProvincia,
            codigoCiudad
        });
        
        scope.Complete();
        return data;
    }
    
}