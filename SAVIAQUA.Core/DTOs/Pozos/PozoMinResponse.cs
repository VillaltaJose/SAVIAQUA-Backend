namespace SAVIAQUA.Core.DTOs.Pozos;

public class PozoMinResponse
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string Junta { get; set; }
    public int CodigoJunta { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    
    public string Provincia { get; set; }
    public string Ciudad { get; set; }
    public string Parroquia { get; set; }
    
    public DateTime FechaCreacion { get; set; }
    
}