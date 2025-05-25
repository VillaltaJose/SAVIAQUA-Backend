using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.API.Filters;
using SAVIAQUA.Core.DTOs.Pozos;
using SAVIAQUA.Core.Filters.Pozos;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter<AuthFilter>]
public class PozosController : ControllerBase
{
    private readonly IPozoService _pozoService;

    public PozosController(IPozoService pozoService)
    {
        _pozoService = pozoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerPozos([FromQuery] ObtenerPozosFilter filter)
    {
        var result = await _pozoService.ObtenerPozos(filter);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CrearPozo([FromBody] NuevoPozoRequest request)
    {
        var result = await _pozoService.CrearNuevoPozo(request);
        return Ok(result);
    }
}