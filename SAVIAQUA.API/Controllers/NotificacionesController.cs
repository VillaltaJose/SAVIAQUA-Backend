using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAVIAQUA.API.Filters;
using SAVIAQUA.Core.Filters.General;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ServiceFilter<AuthFilter>]
public class NotificacionesController : ControllerBase
{
    private readonly INotificacionService _notificacionService;

    public NotificacionesController(INotificacionService notificacionService)
    {
        _notificacionService = notificacionService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerNotificaciones([FromQuery] PaginatedFilter filter)
    {
        var result = await _notificacionService.ObtenerNotificaciones(filter);
        return Ok(result);
    }
    
    [HttpGet("no-leidas")]
    public async Task<IActionResult> ObtenerTotalNoLeidas()
    {
        var result = await _notificacionService.ObtenerTotalNoLeidas();
        return Ok(result);
    }
    
    [HttpPost("{codigoNotificacion:int}/marcar-leida")]
    public async Task<IActionResult> MarcarLeida(int codigoNotificacion)
    {
        var result = await _notificacionService.MarcarLeida(codigoNotificacion);
        return Ok(result);
    }
}