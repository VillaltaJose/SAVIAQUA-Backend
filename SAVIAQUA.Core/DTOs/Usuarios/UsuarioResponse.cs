namespace SAVIAQUA.Core.DTOs.Usuarios;

public class UsuarioResponse
{
    public int Codigo { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public string Rol { get; set; }
    public string Junta { get; set; }
    public DateTime FechaCreacion { get; set; }
}