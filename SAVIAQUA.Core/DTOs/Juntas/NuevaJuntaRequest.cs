using System.Text.Json.Serialization;

namespace SAVIAQUA.Core.DTOs.Juntas;

public class NuevaJuntaRequest
{
    public string Nombre { get; set; }
    public int CodigoProvincia { get; set; }
    public int CodigoCiudad { get; set; }
    public int CodigoParroquia { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Observaciones { get; set; }
}