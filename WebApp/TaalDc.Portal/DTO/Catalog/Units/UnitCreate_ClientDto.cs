using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog;

public class UnitCreate_ClientDto
{
    [JsonPropertyName("unit_id")] public int? UnitId { get; set; }

    [JsonPropertyName("unit_type_id")] public int UnitTypeId { get; set; }

    [JsonPropertyName("scenic_view_id")] public int ScenicViewId { get; set; }

    [JsonPropertyName("unit_identifier")] public string UnitNo { get; set; }

    [JsonPropertyName("floor_id")] public int FloorId { get; set; }

    [JsonPropertyName("floor_area")] public double FloorArea { get; set; }

    [JsonPropertyName("balcony_area")] public double BalconyArea { get; set; }

    [JsonPropertyName("price")] public decimal SellingPrice { get; set; }
    [JsonPropertyName("tower")] public string Tower { get; set; }
}