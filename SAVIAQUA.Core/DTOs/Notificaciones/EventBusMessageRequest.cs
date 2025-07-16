namespace SAVIAQUA.Core.DTOs.Notificaciones;

public class EventBusMessageRequest
{
    public int CodigoCategoria { get; set; }
    public Dictionary<string, dynamic>? ExtraParams { get; set; }
    public int? ReferenciaInt { get; set; }
    public string? ReferenciaStr { get; set; }
}