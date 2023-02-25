using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Sales;

public class GetSalesPaymentResponse
{
    [JsonPropertyName("payment_id")] public int Id { get; set; }

    [JsonPropertyName("paid_on")] public DateTime ActualPaymentDate { get; set; }

    [JsonPropertyName("confirmation_number")]
    public string ConfirmationNumber { get; set; }

    [JsonPropertyName("payment_type_id")] public int PaymentTypeId { get; set; }

    [JsonPropertyName("payment_type")] public string PaymentType { get; set; }

    [JsonPropertyName("transaction_type_id")]
    public int TransactionTypeId { get; set; }

    [JsonPropertyName("transaction_type")] public string TransactionType { get; set; }

    [JsonPropertyName("verified_by")] public string VerifiedBy { get; set; }

    [JsonPropertyName("status_id")] public int PaymentStatusId { get; set; }

    [JsonPropertyName("status")] public string PaymentStatus { get; set; }

    [JsonPropertyName("payment_method")] public string PaymentMethod { get; set; }

    [JsonPropertyName("amount")] public decimal AmountPaid { get; set; }

    [JsonPropertyName("remarks")] public string Remarks { get; set; }

    [JsonPropertyName("correlation_id")] public string CorrelationId { get; set; }

    [JsonPropertyName("order_id")] public int OrderId { get; set; }
}