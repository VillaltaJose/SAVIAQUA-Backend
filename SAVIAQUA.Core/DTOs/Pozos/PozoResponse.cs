namespace SAVIAQUA.Core.DTOs.Pozos;

public class PozoResponse
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string Observaciones { get; set; }
    public DateTime FechaCreacion { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public int CodigoJunta { get; set; }
    public string Junta { get; set; }
    public int CodigoProvincia { get; set; }
    public string Provincia { get; set; }
    public int CodigoCiudad { get; set; }
    public string Ciudad { get; set; }
    public int CodigoParroquia { get; set; }
    public string Parroquia { get; set; }
    
}