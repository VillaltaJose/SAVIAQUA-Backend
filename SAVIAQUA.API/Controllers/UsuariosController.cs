using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.API.Filters;
using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Filters.Usuarios;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter<AuthFilter>]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarNuevoUsuario(NuevoUsuarioRequest request)
    {
        var result = await _usuarioService.RegistrarNuevoUsuario(request);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerUsuarios([FromQuery] ObtenerUsuariosFilter filter)
    {
        var result = await _usuarioService.ObtenerUsuarios(filter);

        return Ok(result);
    }
}