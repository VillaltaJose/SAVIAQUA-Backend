using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Pozos;
using SAVIAQUA.Core.Filters.Pozos;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class PozoService : IPozoService
{
    private readonly IPozoRepository _pozoRepository;

    public PozoService(IPozoRepository pozoRepository)
    {
        _pozoRepository = pozoRepository;
    }

    public async Task<Result<IEnumerable<PozoMinResponse>>> ObtenerPozos(ObtenerPozosFilter filter)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var queryResult = await _pozoRepository.ObtenerPozos(filter);
        
        scope.Complete();

        var result = Result<IEnumerable<PozoMinResponse>>.Ok(queryResult.Item2);
        
        result.Metadata = new Metadata
        {
            CurrentPage = filter.PageNumber,
            TotalCount = queryResult.Item1,
            PageSize = filter.PageSize,
        };

        return result;
    }
}