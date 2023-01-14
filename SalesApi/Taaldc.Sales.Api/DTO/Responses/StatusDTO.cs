using System.Text.Json.Serialization;

namespace Taaldc.Sales.Api.DTO;

public record StatusDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
}