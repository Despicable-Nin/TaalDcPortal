using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Marketing;

public record InquriesResult
{
    public InquriesResult()
    {
        Inquiries = new List<InquiryDto>();
    }

    [JsonPropertyName("inquiries")] public ICollection<InquiryDto> Inquiries { get; set; } 

    [JsonPropertyName("page_size")] public int PageSize { get; set; }

    [JsonPropertyName("page_number")] public int PageNumber { get; set; }

    [JsonPropertyName("total")] public int Total { get; set; }
}