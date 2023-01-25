using System.Text.Json.Serialization;

namespace TaalDc.Portal.Models;

public class PaginationQueryResult<T>
{
    public PaginationQueryResult()
    {
        Data = new List<T>();
    }

    [JsonPropertyName("page_size")] public int PageSize { get; set; }

    [JsonPropertyName("page_number")] public int PageNumber { get; set; }

    [JsonPropertyName("total")] public int TotalCount { get; set; }

    [JsonPropertyName("total_pages")] public int TotalPages { get; set; }

    [JsonPropertyName("data")] public IEnumerable<T> Data { get; set; }
}