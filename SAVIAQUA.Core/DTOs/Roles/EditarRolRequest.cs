namespace SAVIAQUA.Core.DTOs.Roles;

public class EditarRolRequest
{
    public int Codigo { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public bool Activo { get; set; }
    public List<int> Permisos { get; set; }
}