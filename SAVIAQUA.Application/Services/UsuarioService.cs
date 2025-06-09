using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Filters.Usuarios;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordService _passwordService;

    public UsuarioService(IUsuarioRepository usuarioRepository, IPasswordService passwordService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<NuevoUsuarioResponse>> RegistrarNuevoUsuario(NuevoUsuarioRequest request)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var clave = GenerarClave(8);

        var usuario = new Usuario
        {
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Correo = request.Correo,
            CodigoJunta = request.CodigoJunta,
            CodigoRol = request.CodigoRol,
            FechaCreacion = DateTime.Now,
            FechaEdicion = DateTime.Now,
            FechaCambioClave = DateTime.Now,
            HashClave = _passwordService.Hash(clave),
        };

        var codigo = await _usuarioRepository.RegistrarNuevoUsuario(usuario);
        
        scope.Complete();
        return Result<NuevoUsuarioResponse>.Ok(new NuevoUsuarioResponse
        {
            Codigo = codigo,
            Clave = clave
        });
    }
    
    private static string GenerarClave(int longitud)
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        char[] clave = new char[longitud];

        for (int i = 0; i < longitud; i++)
        {
            clave[i] = caracteres[random.Next(caracteres.Length)];
        }

        return new string(clave);
    }


    public async Task<Result<IEnumerable<UsuarioResponse>>> ObtenerUsuarios(ObtenerUsuariosFilter filter)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var usuarios = await _usuarioRepository.ObtenerUsuarios(filter);
        
        scope.Complete();
        return Result<IEnumerable<UsuarioResponse>>.Ok(usuarios);
    }
}