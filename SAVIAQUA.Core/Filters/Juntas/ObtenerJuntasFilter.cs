using SAVIAQUA.Core.Filters.General;

namespace SAVIAQUA.Core.Filters.Juntas;

public class ObtenerJuntasFilter : PaginatedFilter
{
    public int? CodigoProvincia { get; set; }
    public int? CodigoCiudad { get; set; }
    public int? CodigoParroquia { get; set; }
}