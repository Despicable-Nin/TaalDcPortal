using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Catalog.API.DTO;

public class TowerUpsert_HostDto
{
    [JsonPropertyName("property_id")]
    [JsonProperty(Required = Required.Always)]
    public int PropertyId { get; set; }

    [JsonPropertyName("name")]
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; }

    [JsonPropertyName("address")] public string Address { get; set; }

    [JsonPropertyName("tower_id")]
    [JsonProperty(Required = Required.Default)]
    public int? TowerId { get; set; }
}