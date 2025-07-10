using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Pozos;
using SAVIAQUA.Core.Filters.Pozos;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IPozoService
{
    Task<Result<IEnumerable<PozoMinResponse>>> ObtenerPozos(ObtenerPozosFilter filter);

    Task<Result<PozoResponse>> ObtenerPozo(int codigoPozo);

    Task<Result<int>> CrearNuevoPozo(NuevoPozoRequest request);

    Task<Result<IEnumerable<MedicionPozo>>> ObtenerMedicionesFecha(ObtenerMedicionesRequest request);

    Task<Result<MedicionPozo?>> ObtenerUltimaMedicion(int codigoPozo);
}