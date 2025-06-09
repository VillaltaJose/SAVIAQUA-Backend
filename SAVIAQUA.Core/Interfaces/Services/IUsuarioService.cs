using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Filters.Usuarios;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IUsuarioService
{
    Task<Result<NuevoUsuarioResponse>> RegistrarNuevoUsuario(NuevoUsuarioRequest request);
    Task<Result<IEnumerable<UsuarioResponse>>> ObtenerUsuarios(ObtenerUsuariosFilter filter);
}