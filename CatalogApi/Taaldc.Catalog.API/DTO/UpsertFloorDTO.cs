using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Catalog.API.DTO;

public class UpsertFloorDTO
{
    [JsonPropertyName("tower_id")]
    [JsonProperty(Required = Required.Always)]
    public int TowerId { get; set; }
    
    [JsonPropertyName("floor_id")] 
    [JsonProperty(Required = Required.Default)]
    public int? FloorId { get; set; }
    
    [JsonPropertyName("name")]
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; }
    
    [JsonPropertyName("description")] 
    public string Description { get; set; }
	[JsonPropertyName("floor_plan_file_path")]
	public string FloorPlanFilePath { get; set; }
}