using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.API.Filters;
using SAVIAQUA.Core.DTOs.Roles;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter<AuthFilter>]
public class RolesController : ControllerBase
{
    private readonly IRolService _rolService;

    public RolesController(IRolService rolService)
    {
        _rolService = rolService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerRoles()
    {
        var result = await _rolService.ObtenerRoles();
        return Ok(result);
    }
    
    [HttpGet("{codigoRol:int}")]
    public async Task<IActionResult> ObtenerRol(int codigoRol)
    {
        var result = await _rolService.ObtenerRol(codigoRol);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarRol([FromBody] CrearRolRequest request)
    {
        var result = await _rolService.RegistrarRol(request);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> ActualizarRol([FromBody] EditarRolRequest request)
    {
        var result = await _rolService.ActualizarRol(request);
        return Ok(result);
    }
}