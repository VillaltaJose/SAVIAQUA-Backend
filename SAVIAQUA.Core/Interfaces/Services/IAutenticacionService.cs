using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Autenticacion;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IAutenticacionService
{
    Task<Result<SessionResponse>> AutenticarPorCredenciales(LoginRequest credenciales);
}