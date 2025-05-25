using SAVIAQUA.Core.Filters.General;

namespace SAVIAQUA.Core.Filters.Pozos;

public class ObtenerPozosFilter : PaginatedFilter
{
    public int? CodigoProvincia { get; set; }
    public int? CodigoCiudad { get; set; }
    public int? CodigoParroquia { get; set; }
}