namespace SAVIAQUA.Core.DTOs.Notificaciones;

public class NotificacionResponse
{
    public int Codigo { get; set; }
    public int CodigoCategoria { get; set; }
    public DateTime Fecha { get; set; }
    public int? ReferenciaInt { get; set; }
    public string? ReferenciaStr { get; set; }
    public bool Leida { get; set; }
    public string Texto { get; set; }
    public string Categoria { get; set; } = "";
}