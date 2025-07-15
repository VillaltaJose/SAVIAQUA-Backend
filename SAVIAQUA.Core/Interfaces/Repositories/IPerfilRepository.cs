using SAVIAQUA.Core.DTOs.Perfil;
using SAVIAQUA.Core.Entities;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IPerfilRepository
{
    Task<PerfilResponse> ObtenerPerfil(int codigoUsuario);
    Task<bool> ActualizarPerfil(Usuario usuario);
    Task<bool> ActualizarClave(int codigoUsuario, string passwordHash);
    Task<string?> ObtenerHashClave(int codigoUsuario);
}