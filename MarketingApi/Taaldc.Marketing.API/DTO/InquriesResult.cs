using System.Text.Json.Serialization;

namespace Taaldc.Marketing.API.DTO;

public record InquriesResult
{
    [JsonPropertyName("inquiries")] public ICollection<InquiryDto> Inquiries { get; set; }

    [JsonPropertyName("page_size")] public int PageSize { get; set; }

    [JsonPropertyName("page_number")] public int PageNumber { get; set; }

    [JsonPropertyName("total")] public int Total { get; set; }
}