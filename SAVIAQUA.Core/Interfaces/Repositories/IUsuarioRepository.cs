using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Filters.Usuarios;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<int> RegistrarNuevoUsuario(Usuario usuario);
    Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios(ObtenerUsuariosFilter filter);
}