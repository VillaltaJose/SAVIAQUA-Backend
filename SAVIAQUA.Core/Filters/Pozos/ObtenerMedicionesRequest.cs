namespace SAVIAQUA.Core.Filters.Pozos;

public class ObtenerMedicionesRequest
{
    public int CodigoPozo { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}