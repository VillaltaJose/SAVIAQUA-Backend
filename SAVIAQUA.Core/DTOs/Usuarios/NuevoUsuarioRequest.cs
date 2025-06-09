namespace SAVIAQUA.Core.DTOs.Usuarios;

public class NuevoUsuarioRequest
{
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public int CodigoJunta { get; set; }
    public int CodigoRol { get; set; }
}