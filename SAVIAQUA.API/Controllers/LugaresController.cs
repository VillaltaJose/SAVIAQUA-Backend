using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LugaresController : ControllerBase
{
    private readonly ILugarService _lugarService;

    public LugaresController(ILugarService lugarService)
    {
        _lugarService = lugarService;
    }

    [HttpGet("provincias")]
    public async Task<IActionResult> ObtenerProvincias()
    {
        var data = await _lugarService.ObtenerProvincias();

        return Ok(data);
    }
    
    [HttpGet("provincias/{codigoProvincia}/ciudades")]
    public async Task<IActionResult> ObtenerCiudades(int codigoProvincia)
    {
        var data = await _lugarService.ObtenerCiudades(codigoProvincia);

        return Ok(data);
    }
    
    [HttpGet("provincias/{codigoProvincia}/ciudades/{codigoCiudad}/parroquias")]
    public async Task<IActionResult> ObtenerParroquias(int codigoProvincia, int codigoCiudad)
    {
        var data = await _lugarService.ObtenerParroquias(codigoProvincia, codigoCiudad);

        return Ok(data);
    }
}