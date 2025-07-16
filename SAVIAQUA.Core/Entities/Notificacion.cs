namespace SAVIAQUA.Core.Entities;

public class Notificacion
{
    public int Codigo { get; set; }
    public int CodigoUsuario { get; set; }
    public int CodigoCategoria { get; set; }
    public DateTime Fecha { get; set; }
    public int? ReferenciaInt { get; set; }
    public string? ReferenciaStr { get; set; }
    public bool Leida { get; set; }
    public string Texto { get; set; } = "";
}