using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class JuntaService : IJuntaService
{
    private readonly IJuntaRepository _juntaRepository;

    public JuntaService(IJuntaRepository juntaRepository)
    {
        _juntaRepository = juntaRepository;
    }

    public async Task<Result<IEnumerable<JuntaMinResponse>>> ObtenerJuntasMin()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var juntas = await _juntaRepository.ObtenerJuntasMin();
        
        scope.Complete();
        return Result<IEnumerable<JuntaMinResponse>>.Ok(juntas);
    }
}