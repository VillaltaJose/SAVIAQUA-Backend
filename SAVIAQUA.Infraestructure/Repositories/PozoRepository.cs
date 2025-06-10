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
        
        var builder = new SqlBuilder();
        
        if (filter.CodigoJunta is not null)
        {
            builder.Where("p.codigo_junta = :codigoJunta");  
        }

        if (filter.CodigoProvincia is not null)
        {
            builder.Where("p.codigo_provincia = :codigoProvincia");  
        }
        
        if (filter.CodigoCiudad is not null)
        {
            builder.Where("p.codigo_ciudad = :codigoCiudad");  
        }
        
        if (filter.CodigoParroquia is not null)
        {
            builder.Where("p.codigo_parroquia = :codigoParroquia");  
        }
        
        var sqlPozos = builder.AddTemplate(PozosQueries.ObtenerPozos);
        var sqlCount = builder.AddTemplate(PozosQueries.ObtenerTotalPozos);

        var pozos = await _dbConnection.QueryAsync<PozoMinResponse>(sqlPozos.RawSql, filter);
        var count = await _dbConnection.QueryFirstAsync<int>(sqlCount.RawSql, filter);
        
        scope.Complete();
        return (count, pozos);
    }

    public async Task<int> RegistrarNuevoPozo(NuevoPozoRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var codigo = await _dbConnection.ExecuteScalarAsync<int>(PozosQueries.CrearNuevoPozo, request);
        
        scope.Complete();
        return codigo;
    }

    public async Task<IEnumerable<MedicionPozo>> ObtenerMedicionesFecha(ObtenerMedicionesRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var mediciones = await _dbConnection.QueryAsync<MedicionPozo>(PozosQueries.ObtenerMediciones, request);
        
        scope.Complete();
        return mediciones;
    }
}