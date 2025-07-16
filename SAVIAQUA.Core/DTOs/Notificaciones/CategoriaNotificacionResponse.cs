namespace SAVIAQUA.Core.DTOs.Notificaciones;

public class CategoriaNotificacionResponse
{
    public string Plantilla { get; set; } = "";
    public string SqlParametros { get; set; } = "";
    public string SqlDestinatarios { get; set; } = "";
}