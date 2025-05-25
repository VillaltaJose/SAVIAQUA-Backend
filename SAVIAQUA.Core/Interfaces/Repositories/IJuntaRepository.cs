using SAVIAQUA.Core.DTOs.Juntas;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IJuntaRepository
{
    Task<IEnumerable<JuntaMinResponse>> ObtenerJuntasMin();
}