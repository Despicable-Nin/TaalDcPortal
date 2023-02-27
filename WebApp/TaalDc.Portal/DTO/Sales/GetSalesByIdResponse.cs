using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Sales;

public record GetSalesByIdResponse
{
    [JsonPropertyName("buyer_id")] public int? BuyerId { get; init; }
    [JsonPropertyName("salutation")] public string? Salutation { get; init; }

    [JsonPropertyName("first_name")] public string? FirstName { get; init; }

    [JsonPropertyName("last_name")] public string? LastName { get; init; }

    [JsonPropertyName("email_address")] public string? EmailAddress { get; init; }

    [JsonPropertyName("contact_number")] public string? PhoneNo { get; init; }

    [JsonPropertyName("mobile_number")] public string? MobileNo { get; init; }

    [JsonPropertyName("address")] public string? Street { get; init; }
    [JsonPropertyName("country")] public string? Country { get; init; }

    [JsonPropertyName("province")] public string? State { get; init; }

    [JsonPropertyName("towncity")] public string? City { get; init; }

    [JsonPropertyName("zip_code")] public string? ZipCode { get; init; }

    [JsonPropertyName("order_id")] public int? OrderId { get; init; }

    [JsonPropertyName("transaction_date")] public DateTime? TransactionDate { get; init; }

    [JsonPropertyName("code")] public string? Code { get => this.OrderId.Value.ToString("00000"); }

	[JsonPropertyName("broker")] public string? Broker { get; init; }

    [JsonPropertyName("final_price")] public decimal? FinalPrice { get; init; }

    [JsonPropertyName("refundable")] public bool? IsRefundable { get; init; }

    [JsonPropertyName("status")] public string? Status { get; init; }

    [JsonPropertyName("status_id")] public int? StatusId { get; init; }

    [JsonPropertyName("remarks")] public string? Remarks { get; init; }

    [JsonPropertyName("property")] public string? Property { get; init; }

    [JsonPropertyName("property_id")] public int? PropertyId { get; init; }

    [JsonPropertyName("tower")] public string? Tower { get; init; }

    [JsonPropertyName("tower_id")] public int? TowerId { get; init; }

    [JsonPropertyName("floor")] public string? Floor { get; init; }

    [JsonPropertyName("floor_id")] public int? FloorId { get; init; }

    [JsonPropertyName("unit")] public string? Unit { get; init; }

    [JsonPropertyName("unit_id")] public int? UnitId { get; init; }

    [JsonPropertyName("view_id")] public string? ScenicView { get; init; }

    [JsonPropertyName("view")] public int? ScenicViewId { get; init; }

    [JsonPropertyName("unit_type")] public string UnitType { get; init; }

    [JsonPropertyName("unit_type_id")] public int UnitTypeId { get; init; }

    [JsonPropertyName("unit_status")] public string UnitStatus { get; init; }

    [JsonPropertyName("unit_status_id")] public int UnitStatusId { get; init; }

    [JsonPropertyName("original_price")] public decimal OriginalPrice { get; init; }

    [JsonPropertyName("selling_price")] public decimal SellingPrice { get; init; }

    [JsonPropertyName("unit_area")] public double UnitArea { get; init; }

    [JsonPropertyName("balcony_area")] public double BalconyArea { get; init; }
}