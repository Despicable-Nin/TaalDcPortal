using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.Units;

public record PaginatedAvailableUnitsQueryResult
{
    public PaginatedAvailableUnitsQueryResult(int pageSize, int pageNumber, int totalCount,
        IEnumerable<AvailableUnitQueryResult> units)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        Units = units;
    }

    [JsonPropertyName("page_size")] public int PageSize { get; }

    [JsonPropertyName("page_number")] public int PageNumber { get; }

    [JsonPropertyName("total")] public int TotalCount { get; }

    [JsonPropertyName("units")] public IEnumerable<AvailableUnitQueryResult> Units { get; }
}