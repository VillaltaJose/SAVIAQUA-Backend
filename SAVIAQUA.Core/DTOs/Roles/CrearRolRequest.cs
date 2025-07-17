namespace SAVIAQUA.Core.DTOs.Roles;

public class CrearRolRequest
{
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public bool Activo { get; set; }
    public List<int> Permisos { get; set; }
}