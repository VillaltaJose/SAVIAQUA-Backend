using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Filters.Usuarios;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<IEnumerable<UsuarioResponse>>> ObtenerUsuarios(ObtenerUsuariosFilter filter)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var usuarios = await _usuarioRepository.ObtenerUsuarios(filter);
        
        scope.Complete();
        return Result<IEnumerable<UsuarioResponse>>.Ok(usuarios);
    }
}