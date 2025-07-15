using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Perfil;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IPerfilService
{
    Task<Result<PerfilResponse>> ObtenerMiPerfil();
    Task<Result<bool>> ActualizarPerfil(ActualizarPerfilRequest request);
    Task<Result<bool>> ActualizarClave(ActualizarClaveRequest request);
}