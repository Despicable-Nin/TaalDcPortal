using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Catalog;

public class UnitType_ClientDto
{
    [JsonPropertyName("unit_type_id")] public int Id { get; set; }

    [JsonPropertyName("unit_type_name")] public string Name { get; set; }

    [JsonPropertyName("unit_type_short_code")]
    public string ShortCode { get; set; }
}