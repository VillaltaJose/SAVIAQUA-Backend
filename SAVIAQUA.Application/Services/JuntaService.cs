using SAVIAQUA.Core.App;
using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Filters.Juntas;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class JuntaService : IJuntaService
{
    private readonly IJuntaRepository _juntaRepository;
    private readonly Session _session;

    public JuntaService(IJuntaRepository juntaRepository, Session session)
    {
        _juntaRepository = juntaRepository;
        _session = session;
    }

    public async Task<Result<IEnumerable<JuntaMinResponse>>> ObtenerJuntasMin()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var juntas = await _juntaRepository.ObtenerJuntasMin();
        
        scope.Complete();
        return Result<IEnumerable<JuntaMinResponse>>.Ok(juntas);
    }

    public async Task<Result<IEnumerable<JuntaResponse>>> ObtenerJuntas(ObtenerJuntasFilter filtros)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var queryResult = await _juntaRepository.ObtenerJuntas(filtros);
        
        scope.Complete();
        
        var result = Result<IEnumerable<JuntaResponse>>.Ok(queryResult.Item2);
        
        result.Metadata = new Metadata
        {
            CurrentPage = filtros.PageNumber,
            TotalCount = queryResult.Item1,
            PageSize = filtros.PageSize,
        };

        return result;
    }
    
    public async Task<Result<int>> CrearNuevaJunta(NuevaJuntaRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var codigo = await _juntaRepository.RegistrarNuevaJunta(request);
        
        scope.Complete();
        return Result<int>.Ok(codigo);
    }
}