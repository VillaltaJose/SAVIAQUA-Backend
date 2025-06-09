using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Filters.Juntas;
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

    public async Task<(int, IEnumerable<JuntaResponse>)> ObtenerJuntas(ObtenerJuntasFilter filtros)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var builder = new SqlBuilder();

        if (filtros.CodigoProvincia is not null)
        {
            builder.Where("j.codigo_provincia = :codigoProvincia");
        }
        
        if (filtros.CodigoCiudad is not null)
        {
            builder.Where("j.codigo_ciudad = :codigoCiudad");
        }
        
        if (filtros.CodigoParroquia is not null)
        {
            builder.Where("j.codigo_parroquia = :codigoParroquia");
        }
        
        var sqlJuntas = builder.AddTemplate(JuntasQueries.ObtenerJuntas);
        var sqlCount = builder.AddTemplate(JuntasQueries.ObtenerTotalJuntas);

        var juntas = await _dbConnection.QueryAsync<JuntaResponse>(sqlJuntas.RawSql, filtros);
        var count = await _dbConnection.QueryFirstAsync<int>(sqlCount.RawSql, filtros);
        
        scope.Complete();
        return (count, juntas);
    }
    
    public async Task<int> RegistrarNuevaJunta(NuevaJuntaRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var codigo = await _dbConnection.ExecuteScalarAsync<int>(JuntasQueries.CrearNuevaJunta, request);
        
        scope.Complete();
        return codigo;
    }
}