using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Filters.Usuarios;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios(ObtenerUsuariosFilter filter);
}