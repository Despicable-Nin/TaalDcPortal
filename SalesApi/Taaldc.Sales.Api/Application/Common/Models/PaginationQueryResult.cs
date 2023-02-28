using System.Text.Json.Serialization;
using Taaldc.Sales.Api.Application.Queries;

namespace Taaldc.Sales.Api.Application.Common.Models;

public class PaginationQueryResult<T> : IResultDto
{
    public PaginationQueryResult(int pageSize, int pageNumber, int totalCount = 0, IEnumerable<T> data = default)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        TotalPages = totalCount % pageSize > 0 ? totalCount / pageSize + 1 : totalCount / pageSize;
        Data = data;
    }

    [JsonPropertyName("page_size")] public int PageSize { get; }

    [JsonPropertyName("page_number")] public int PageNumber { get; }

    [JsonPropertyName("total")] public int TotalCount { get; }

    [JsonPropertyName("total_pages")] public int TotalPages { get; }

    [JsonPropertyName("data")] public IEnumerable<T> Data { get; }
}