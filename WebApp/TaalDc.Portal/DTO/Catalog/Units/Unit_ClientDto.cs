using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Catalog;

public class Unit_ClientDto
{
    [JsonPropertyName("unit_id")] public int Id { get; set; }

    [JsonPropertyName("property_id")] public int PropertyId { get; set; }

    [JsonPropertyName("property_name")] public string PropertyName { get; set; }

    [JsonPropertyName("tower_id")] public int TowerId { get; set; }

    [JsonPropertyName("tower_name")] public string TowerName { get; set; }
    [JsonPropertyName("tower_no")] public string Tower { get; set; }
    [JsonPropertyName("unit_type_id")] public int UnitTypeId { get; set; }

    [JsonPropertyName("unit_type")] public string UnitType { get; set; }

    [JsonPropertyName("scenic_view_id")] public int ScenicViewId { get; set; }

    [JsonPropertyName("scenic_view")] public string ScenicView { get; set; }

    [JsonPropertyName("floor_id")] public int FloorId { get; set; }

    [JsonPropertyName("floor_name")] public string FloorName { get; set; }

    [JsonPropertyName("total_area")] public double TotalArea { get; set; }

    [JsonPropertyName("floor_area")] public double FloorArea { get; set; }

    [JsonPropertyName("balcony_area")] public double BalconyArea { get; set; }

    [JsonPropertyName("identifier")] public string Identifier { get; set; }

    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("unit_status_id")] public int UnitStatusId { get; set; }

    [JsonPropertyName("unit_status")] public string UnitStatus { get; set; }

    [JsonPropertyName("is_active")] public bool IsActive { get; set; }
}