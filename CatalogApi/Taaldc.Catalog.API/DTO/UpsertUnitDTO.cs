using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.DTO;

public class UpsertUnitDTO
{
    [JsonPropertyName("unit_id")] public int? UnitId { get; }
    [JsonPropertyName("unit_type_id")] public int UnitTypeId { get; }
    [JsonPropertyName("scenic_view_id")] public int ScenicViewId { get; }
    [JsonPropertyName("unit_identifier")] public string UnitNo { get; }
    [JsonPropertyName("floor_area")] public double FloorArea { get; }
    [JsonPropertyName("price")] public decimal Price { get; }
}