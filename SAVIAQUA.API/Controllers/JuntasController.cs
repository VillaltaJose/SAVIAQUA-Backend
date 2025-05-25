using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> ObtenerJuntas([FromQuery] bool minified)
    {
        if (minified)
        {
            var juntasMin = await _juntaService.ObtenerJuntasMin();
            return Ok(juntasMin);
        }

        return Ok();
    }
}