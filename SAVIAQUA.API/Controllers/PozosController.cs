using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.Core.Filters.Pozos;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
}