using System.Text.Json.Serialization;

namespace SAVIAQUA.Core.Filters.General;

public class PaginatedSessionFilter : SessionFilter
{
    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    [JsonIgnore]
    public int Offset { get; set; } = 0;
}