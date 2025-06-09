using SAVIAQUA.Core.App;
using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Perfil;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class PerfilService : IPerfilService
{
    private readonly IPerfilRepository _perfilRepository;
    private readonly Session _session;

    public PerfilService(IPerfilRepository perfilRepository, Session session)
    {
        _perfilRepository = perfilRepository;
        _session = session;
    }

    public async Task<Result<PerfilResponse>> ObtenerMiPerfil()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var perfil = await _perfilRepository.ObtenerPerfil(_session.CodigoUsuario);
        
        scope.Complete();
        return Result<PerfilResponse>.Ok(perfil);
    }

    public async Task<Result<bool>> ActualizarPerfil(ActualizarPerfilRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var usuario = new Usuario
        {
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Correo = request.Correo,
            FechaEdicion = DateTime.UtcNow,
            Codigo = _session.CodigoUsuario,
        };

        var actualizado = await _perfilRepository.ActualizarPerfil(usuario);
        
        scope.Complete();
        return Result<bool>.Ok(actualizado);
    }
}