using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Sales.Contracts;

public class GetContractResponse
{
    [JsonPropertyName(("code"))]
    public string Code { get; init; }
    [JsonPropertyName("buyer_id")] 
    public int BuyerId { get;  init; }
    [JsonPropertyName("transaction_date")] 
    public DateTime? TransactionDate { get;  init; }
    [JsonPropertyName("broker")] 
    public string Broker { get;  init; }
    [JsonPropertyName("reservation_expires_on")] 
    public DateTime? ReservationExpiresOn { get; init; }
    [JsonPropertyName("payment_method")] 
    public string PaymentMethod { get; init; }
    [JsonPropertyName("remarks")] 
    public string Remarks { get;  init; }
    [JsonPropertyName("order_status")] 
    public string Status { get; init; }
    [JsonPropertyName("order_status_id")] 
    public int StatusId { get; init; }
    [JsonPropertyName("discount")] 
    public decimal Discount { get; init; }
    [JsonPropertyName("price")] 
    public decimal Price { get; init; }
    [JsonPropertyName("unit_id")] 
    public int UnitId { get; init; }
    [JsonPropertyName("confirmation_number")] 
    public string ConfirmationNumber { get; init; }
    [JsonPropertyName("payment_date")] 
    public DateTime ActualPaymentDate { get; init; }
    [JsonPropertyName("payment_status_id")] 
    public int PaymentStatusId { get; init; }
    [JsonPropertyName("payment_status")] 
    public string PaymentStatus { get; init; }
    [JsonPropertyName("payment_type_id")] 
    public int PaymentTypeId { get; init; }
    [JsonPropertyName("payment_type")] 
    public string PaymentType { get; init; }
    [JsonPropertyName("transaction_type")] 
    public string TransactionType { get; init; }
    [JsonPropertyName("transaction_type_id")] 
    public int TransactionTypeId { get; init; }
    [JsonPropertyName("verified_by")] 
    public string VerifiedBy { get; init; }
    [JsonPropertyName("amount_paid")] 
    public decimal AmountPaid { get; init; }
}