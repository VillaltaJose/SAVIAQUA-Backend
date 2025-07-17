namespace SAVIAQUA.Core.DTOs.Roles;

public class RolResponse
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Activo { get; set; }
    
    public List<int> Permisos { get; set; }
}