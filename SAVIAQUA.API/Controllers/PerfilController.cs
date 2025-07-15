using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.API.Filters;
using SAVIAQUA.Core.DTOs.Perfil;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter<AuthFilter>]
public class PerfilController : ControllerBase
{
    private readonly IPerfilService _perfilService;

    public PerfilController(IPerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerMiPerfil()
    {
        var result = await _perfilService.ObtenerMiPerfil();
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> ActualizarPerfil([FromBody] ActualizarPerfilRequest request)
    {
        var result = await _perfilService.ActualizarPerfil(request);
        return Ok(result);
    }
    
    [HttpPut("clave")]
    public async Task<IActionResult> ActualizarClave([FromBody] ActualizarClaveRequest request)
    {
        var result = await _perfilService.ActualizarClave(request);
        return Ok(result);
    }
}