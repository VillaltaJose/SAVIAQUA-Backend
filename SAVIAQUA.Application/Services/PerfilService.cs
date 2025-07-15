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
    private readonly IPasswordService _passwordService;

    public PerfilService(IPerfilRepository perfilRepository, Session session, IPasswordService passwordService)
    {
        _perfilRepository = perfilRepository;
        _session = session;
        _passwordService = passwordService;
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
    
    public async Task<Result<bool>> ActualizarClave(ActualizarClaveRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var currentHash = await _perfilRepository.ObtenerHashClave(_session.CodigoUsuario);

        if (string.IsNullOrEmpty(currentHash))
        {
            return Result<bool>.Fail("Usuario no encontrado");
        }

        if (string.IsNullOrEmpty(request.Nueva))
        {
            return Result<bool>.Fail("La nueva clave no puede ser vacía");
        }
        
        var isPasswordValid = _passwordService.Check(currentHash, request.Actual);

        if (!isPasswordValid)
        {
            return Result<bool>.Fail("La clave ingresada es incorrecta");
        }
        
        var updated = await _perfilRepository.ActualizarClave(_session.CodigoUsuario, _passwordService.Hash(request.Nueva));
        
        scope.Complete();

        return Result<bool>.Ok(updated);
    }
}