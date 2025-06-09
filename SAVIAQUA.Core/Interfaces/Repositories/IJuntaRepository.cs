using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Filters.Juntas;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IJuntaRepository
{
    Task<IEnumerable<JuntaMinResponse>> ObtenerJuntasMin();
    Task<(int, IEnumerable<JuntaResponse>)> ObtenerJuntas(ObtenerJuntasFilter filtros);
    Task<int> RegistrarNuevaJunta(NuevaJuntaRequest request);
}