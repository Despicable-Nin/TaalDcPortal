using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Catalog;

public class Floor_ClientDto
{
    [JsonPropertyName("floor_id")] public int Id { get; set; }

    [JsonPropertyName("tower_id")] public int TowerId { get; set; }

    [JsonPropertyName("property_id")] public int PropertyId { get; set; }

    [JsonPropertyName("property_name")] public string PropertyName { get; set; }

    [JsonPropertyName("tower_name")] public string TowerName { get; set; }

    [JsonPropertyName("floor_name")] public string FloorName { get; set; }

    [JsonPropertyName("floor_description")]
    public string FloorDescription { get; set; }

    [JsonPropertyName("floor_plan_file_path")]
    public string FloorPlanFilePath { get; set; }

    [JsonPropertyName("units")] public int Units { get; set; }
}