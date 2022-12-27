using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.DTO;

public class UpsertTowerDTO
{
    [JsonPropertyName("name")] public string Name { get;  set; }
    [JsonPropertyName("address")] public string Address { get;  set; }
    [JsonPropertyName("tower_id")] public int? TowerId { get;  set; }
}