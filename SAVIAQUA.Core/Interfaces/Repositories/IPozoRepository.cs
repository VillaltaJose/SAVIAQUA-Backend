using SAVIAQUA.Core.DTOs.Pozos;
using SAVIAQUA.Core.Filters.Pozos;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IPozoRepository
{
    Task<(int, IEnumerable<PozoMinResponse>)> ObtenerPozos(ObtenerPozosFilter filter);

    Task<int> RegistrarNuevoPozo(NuevoPozoRequest request);
}