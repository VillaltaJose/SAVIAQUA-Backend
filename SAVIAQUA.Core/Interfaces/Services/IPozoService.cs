using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Pozos;
using SAVIAQUA.Core.Filters.Pozos;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IPozoService
{
    Task<Result<IEnumerable<PozoMinResponse>>> ObtenerPozos(ObtenerPozosFilter filter);
}