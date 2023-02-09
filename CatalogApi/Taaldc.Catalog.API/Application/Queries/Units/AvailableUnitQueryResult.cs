using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.Units;

public record AvailableUnitQueryResult
{
    [JsonPropertyName("unit_id")] public int Id { get; }

    [JsonPropertyName("unit_identifier")] public string Identifier { get; }

    [JsonPropertyName("price")] public decimal Price { get; }

    [JsonPropertyName("floor_area")] public double FloorArea { get; }

    [JsonPropertyName("floor")] public string Floor { get; }

    [JsonPropertyName("floor_description")]
    public string FloorDesc { get; }

    [JsonPropertyName("view")] public string View { get; }

    [JsonPropertyName("status")] public string Status { get; }

    [JsonPropertyName("type")] public string Type { get; }

    [JsonPropertyName("type_short_code")] public string TypeCode { get; }

    [JsonPropertyName("remarks")] public string Remarks { get; }
}