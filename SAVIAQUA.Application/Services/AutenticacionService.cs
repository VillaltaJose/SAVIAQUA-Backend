using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Autenticacion;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;
using SAVIAQUA.Core.Options;

namespace SAVIAQUA.Application.Services;

public class AutenticacionService : IAutenticacionService
{
    private readonly IPasswordService _passwordService;
    private readonly IAutenticacionRepository _autenticacionRepository;
    private readonly SecurityOptions _securityOptions;

    public AutenticacionService(
        IPasswordService passwordService,
        IAutenticacionRepository autenticacionRepository,
        IOptions<SecurityOptions> options
    )
    {
        _passwordService = passwordService;
        _autenticacionRepository = autenticacionRepository;
        _securityOptions = options.Value;
    }

    public async Task<Result<SessionResponse>> AutenticarPorCredenciales(LoginRequest credenciales)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var usuario = await _autenticacionRepository.ObtenerUsuarioPorCorreo(credenciales.Correo);

        if (usuario is null)
        {
            return Result<SessionResponse>.Fail("El correo o la clave ingresados son incorrectos");
        }
        
        var claveValida = _passwordService.Check(usuario.HashClave, credenciales.Clave);

        if (!claveValida)
        {
            scope.Complete();
            return Result<SessionResponse>.Fail("El correo o la clave ingresados son incorrectos");
        }
        
        var sesionResponse = await CompletarLogin(usuario);
        scope.Complete();

        return Result<SessionResponse>.Ok(sesionResponse);
    }
    
    private async Task<SessionResponse> CompletarLogin(Usuario usuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();
        
        var sessionId = Guid.NewGuid();

        var jwt = GenerateToken(usuario, _securityOptions.JwtMinutesTime, sessionId: sessionId.ToString());
        var refreshToken = GenerateToken(usuario, _securityOptions.RefreshTokenMinutesTime, sessionId: sessionId.ToString());

        // TODO: Registrar Sesion

        scope.Complete();
        
        return new SessionResponse
        {
            PerfilUsuario = new PerfilUsuarioResponse
            {
                Codigo = usuario.Codigo,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo
            },
            JWT = jwt,
            RefreshToken = refreshToken,
        };
    }
    
    private string GenerateToken(Usuario usuario, int? minutos = null, string? sessionId = "")
    {
        minutos ??= _securityOptions.JwtMinutesTime;

        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_securityOptions.SecretKey)
        );

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var claims = new List<Claim>
        {
            new ("Nombres", $"{usuario.Nombres}"),
            new ("Apellidos", $"{usuario.Apellidos}"),
            new ("Correo", usuario.Correo),
            new ("UId", usuario.Codigo.ToString())
        };

        if (!string.IsNullOrWhiteSpace(sessionId))
        {
            claims.Add(new Claim("SessionId", sessionId));
        }

        var payload = new JwtPayload
        (
            _securityOptions.Issuer,
            _securityOptions.Audience,
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes((float) minutos)
        );

        var token = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}