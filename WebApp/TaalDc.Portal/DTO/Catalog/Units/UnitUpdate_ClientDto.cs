using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class UnitUpdate_ClientDto
{
    [JsonPropertyName("unit_id")] public int UnitId { get; set; }

    [JsonPropertyName("unit_type_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitTypeId { get; set; }

    [JsonPropertyName("scenic_view_id")]
    [JsonProperty(Required = Required.Always)]
    public int ScenicViewId { get; set; }

    [JsonPropertyName("unit_identifier")]
    [JsonProperty(Required = Required.Always)]
    public string UnitNo { get; set; }

    [JsonPropertyName("floor_id")]
    [JsonProperty(Required = Required.Always)]
    public int FloorId { get; set; }

    [JsonPropertyName("floor_area")] public double FloorArea { get; set; }

    [JsonPropertyName("balcony_area")] public double BalconyArea { get; set; }

    [JsonPropertyName("price")]
    [JsonProperty(Required = Required.Always)]
    public decimal SellingPrice { get; set; }

    [JsonPropertyName("unit_status_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitStatusId { get; set; }

    [JsonPropertyName("is_active")]
    [JsonProperty(Required = Required.Always)]
    public bool IsActive { get; set; }

    [JsonPropertyName("remarks")] public string Remarks { get; set; }
}