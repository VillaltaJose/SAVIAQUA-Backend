using SAVIAQUA.Core.App;
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
    private readonly Session _session;

    public PozoService(IPozoRepository pozoRepository, Session session)
    {
        _pozoRepository = pozoRepository;
        _session = session;
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
    
    public async Task<Result<PozoResponse>> ObtenerPozo(int codigoPozo)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var pozo = await _pozoRepository.ObtenerPozo(codigoPozo);

        if (pozo is null)
        {
            return Result<PozoResponse>.Fail("Pozo no encontrado");
        }
        
        scope.Complete();

        return Result<PozoResponse>.Ok(pozo);
    }

    public async Task<Result<int>> CrearNuevoPozo(NuevoPozoRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        request.CodigoUsuarioRegistra = _session.CodigoUsuario;

        var codigo = await _pozoRepository.RegistrarNuevoPozo(request);
        
        scope.Complete();
        return Result<int>.Ok(codigo);
    }

    public async Task<Result<IEnumerable<MedicionPozo>>> ObtenerMedicionesFecha(ObtenerMedicionesRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var mediciones = await _pozoRepository.ObtenerMedicionesFecha(request);
        
        scope.Complete();
        return Result<IEnumerable<MedicionPozo>>.Ok(mediciones);
    }
}