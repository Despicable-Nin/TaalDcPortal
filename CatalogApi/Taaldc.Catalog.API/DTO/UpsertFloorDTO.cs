using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.DTO;

public class UpsertFloorDTO
{
    [JsonPropertyName("floor_id")] public int? FloorId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
}