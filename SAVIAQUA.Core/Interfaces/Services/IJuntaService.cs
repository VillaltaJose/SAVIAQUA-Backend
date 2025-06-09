using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Filters.Juntas;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IJuntaService
{
    Task<Result<IEnumerable<JuntaMinResponse>>> ObtenerJuntasMin();
    Task<Result<IEnumerable<JuntaResponse>>> ObtenerJuntas(ObtenerJuntasFilter filtros);
    Task<Result<int>> CrearNuevaJunta(NuevaJuntaRequest request);
}