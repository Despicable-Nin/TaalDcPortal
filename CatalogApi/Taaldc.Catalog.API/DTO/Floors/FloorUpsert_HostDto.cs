using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Catalog.API.DTO;

public class FloorUpsert_HostDto
{
    public FloorUpsert_HostDto(int towerId, int? floorId, string name, string description, string floorPlanFilePath)
    {
        TowerId = towerId;
        FloorId = floorId;
        Name = name;
        Description = description;
        FloorPlanFilePath = floorPlanFilePath;
    }

    public FloorUpsert_HostDto()
    {
    }

    [JsonPropertyName("tower_id")]
    [JsonProperty(Required = Required.Always)]
    public int TowerId { get; set; }

    [JsonPropertyName("floor_id")]
    [JsonProperty(Required = Required.Default)]
    public int? FloorId { get; set; }

    [JsonPropertyName("name")]
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("floor_plan_file_path")]
    public string FloorPlanFilePath { get; set; }
}