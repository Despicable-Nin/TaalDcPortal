using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.Towers;

public class TowerQueryResult
{
    [JsonPropertyName("tower_id")] public int Id { get; set; }

    [JsonPropertyName("property_id")] public int PropertyId { get; set; }

    [JsonPropertyName("property_name")] public string PropertyName { get; set; }

    [JsonPropertyName("tower_name")] public string TowerName { get; set; }

    [JsonPropertyName("address")] public string Address { get; set; }

    [JsonPropertyName("units")] public int Units { get; set; }
}