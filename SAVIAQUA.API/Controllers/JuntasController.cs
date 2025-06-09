using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.Core.DTOs.Juntas;
using SAVIAQUA.Core.Filters.Juntas;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JuntasController : ControllerBase
{
    private readonly IJuntaService _juntaService;

    public JuntasController(IJuntaService juntaService)
    {
        _juntaService = juntaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerJuntas([FromQuery] ObtenerJuntasFilter filtros, [FromQuery] bool minified)
    {
        if (minified)
        {
            var juntasMin = await _juntaService.ObtenerJuntasMin();
            return Ok(juntasMin);
        }

        var juntas = await _juntaService.ObtenerJuntas(filtros);
        return Ok(juntas);
    }
    
    [HttpPost]
    public async Task<IActionResult> CrearPozo([FromBody] NuevaJuntaRequest request)
    {
        var result = await _juntaService.CrearNuevaJunta(request);
        return Ok(result);
    }
}