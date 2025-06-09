namespace SAVIAQUA.Core.DTOs.Juntas;

public class JuntaResponse
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string UrlLogo { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Provincia { get; set; }
    public string Ciudad { get; set; }
    public string Parroquia { get; set; }
}