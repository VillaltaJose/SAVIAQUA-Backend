namespace SAVIAQUA.Core.Entities;

public class Usuario
{
    public int Codigo { get; set; }
    
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public string HashClave { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaEdicion { get; set; }
    public DateTime FechaCambioClave { get; set; }
    
    public int CodigoRol { get; set; }
    public int CodigoJunta { get; set; }
}