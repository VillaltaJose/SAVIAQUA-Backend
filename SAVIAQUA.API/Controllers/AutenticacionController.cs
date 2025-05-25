using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.Core.DTOs.Autenticacion;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{
    private readonly IAutenticacionService _autenticacionService;

    public AutenticacionController(IAutenticacionService autenticacionService)
    {
        _autenticacionService = autenticacionService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginCredenciales(LoginRequest request)
    {
        var result = await _autenticacionService.AutenticarPorCredenciales(request);

        if (result.Success)
        {
            SetRefreshTokenCookie(result.Value.RefreshToken);
        }
        
        return Ok(result);
    }
    
    private void SetRefreshTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            // TODO: agregar dominio
        };

        HttpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
}