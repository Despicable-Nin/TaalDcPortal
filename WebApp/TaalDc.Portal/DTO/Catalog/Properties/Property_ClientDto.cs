using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Catalog;

public class Property_ClientDto
{
    [JsonPropertyName("property_id")] public int Id { get; set; }

    [JsonPropertyName("project_id")] public int ProjectId { get; set; }

    [JsonPropertyName("property_name")] public string PropertyName { get; set; }

    [JsonPropertyName("land_area")] public double LandArea { get; set; }

    [JsonPropertyName("towers")] public int Towers { get; set; }
}