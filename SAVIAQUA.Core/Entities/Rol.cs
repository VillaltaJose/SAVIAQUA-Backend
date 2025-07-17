namespace SAVIAQUA.Core.Entities;

public class Rol
{
    public int Codigo { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaEdicion { get; set; }
}