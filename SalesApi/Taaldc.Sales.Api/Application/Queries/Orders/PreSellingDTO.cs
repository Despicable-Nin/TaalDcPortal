using System.Text.Json.Serialization;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record PreSellingDTO
{
    [JsonPropertyName("salutation")] public string Salutation { get; set; }

    [JsonPropertyName("first_name")] public string FirstName { get; set; }

    [JsonPropertyName("last_name")] public string LastName { get; set; }

    [JsonPropertyName("email_address")] public string EmailAddress { get; set; }

    [JsonPropertyName("contact_number")] public string ContactNo { get; set; }

    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("province")] public string Province { get; set; }

    [JsonPropertyName("towncity")] public string TownCity { get; set; }

    [JsonPropertyName("zip_code")] public string ZipCode { get; set; }

    [JsonPropertyName("order_id")] public string OrderId { get; set; }

    [JsonPropertyName("code")] public string Code { get; set; }

    [JsonPropertyName("broker")] public string Broker { get; set; }

    [JsonPropertyName("final_price")] public decimal FinalPrice { get; set; }

    [JsonPropertyName("refundable")] public bool IsRefundable { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("status_id")] public int StatusId { get; set; }

    [JsonPropertyName("remarks")] public string Remarks { get; set; }

    [JsonPropertyName("property")] public string Property { get; set; }

    [JsonPropertyName("property_id")] public int PropertyId { get; set; }

    [JsonPropertyName("tower")] public string Tower { get; set; }

    [JsonPropertyName("tower_id")] public int TowerId { get; set; }

    [JsonPropertyName("floor")] public string Floor { get; set; }

    [JsonPropertyName("floor_id")] public int FloorId { get; set; }

    [JsonPropertyName("unit")] public string Unit { get; set; }

    [JsonPropertyName("unit_id")] public int UnitId { get; set; }

    [JsonPropertyName("view_id")] public string ScenicView { get; set; }

    [JsonPropertyName("view")] public int ScenicViewId { get; set; }

    [JsonPropertyName("unit_type")] public string UnitType { get; set; }

    [JsonPropertyName("unit_type_id")] public int UnitTypeId { get; set; }

    [JsonPropertyName("unit_status")] public string UnitStatus { get; set; }

    [JsonPropertyName("unit_status_id")] public int UnitStatusId { get; set; }

    [JsonPropertyName("original_price")] public decimal OriginalPrice { get; set; }

    [JsonPropertyName("selling_price")] public decimal SellingPrice { get; set; }

    [JsonPropertyName("unit_area")] public double UnitArea { get; set; }

    [JsonPropertyName("balcony_area")] public double BalconyArea { get; set; }
}