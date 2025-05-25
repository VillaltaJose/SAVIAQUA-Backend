using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Juntas;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IJuntaService
{
    Task<Result<IEnumerable<JuntaMinResponse>>> ObtenerJuntasMin();
}