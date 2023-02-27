using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Catalog.API.DTO;

public class UnitUpsert_HostDto
{
    [JsonPropertyName("unit_id")]
    [JsonProperty(Required = Required.Default)]
    public int? UnitId { get; set; }

    [JsonPropertyName("unit_type_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitTypeId { get; set; }

    [JsonPropertyName("scenic_view_id")]
    [JsonProperty(Required = Required.Always)]
    public int ScenicViewId { get; set; }

    [JsonPropertyName("unit_identifier")]
    [JsonProperty(Required = Required.Always)]
    public string UnitNo { get; set; }

    [JsonPropertyName("floor_area")]
    [JsonProperty(Required = Required.Always)]
    public double FloorArea { get; set; }

    [JsonPropertyName("balcony_area")]
    [JsonProperty(Required = Required.Always)]
    public double BalconyArea { get; set; }

    [JsonPropertyName("price")]
    [JsonProperty(Required = Required.Always)]
    public decimal Price { get; set; }

    [JsonPropertyName("floor_id")]
    [JsonProperty(Required = Required.Always)]
    public int FloorId { get; set; }

    [JsonPropertyName("unit_status_id")] public int? UnitStatusId { get; set; }

    [JsonPropertyName("is_active")] public bool IsActive { get; set; }


    [JsonPropertyName("remarks")] public string Remarks { get; set; }
}