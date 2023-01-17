using System.Text.Json.Serialization;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record PaymentDTO
{
    public PaymentDTO(int id, DateTime actualPaymentDate, string confirmationNumber, int paymentTypeId,
        string paymentType, int transactionTypeId, string verifiedBy, int paymentStatusId, string paymentStatus,
        string paymentMethod, decimal amountPaid, string remarks, int orderId, string correlationId,
        string transactionType)
    {
        Id = id;
        ActualPaymentDate = actualPaymentDate;
        ConfirmationNumber = confirmationNumber;
        PaymentTypeId = paymentTypeId;
        PaymentType = paymentType;
        TransactionTypeId = transactionTypeId;
        VerifiedBy = verifiedBy;
        PaymentStatusId = paymentStatusId;
        PaymentStatus = paymentStatus;
        PaymentMethod = paymentMethod;
        AmountPaid = amountPaid;
        Remarks = remarks;
        CorrelationId = correlationId;
        OrderId = orderId;
        transactionType = transactionType;
    }

    [JsonPropertyName("payment_id")] public int Id { get; }

    [JsonPropertyName("paid_on")] public DateTime ActualPaymentDate { get; }

    [JsonPropertyName("confirmation_number")]
    public string ConfirmationNumber { get; }

    [JsonPropertyName("payment_type_id")] public int PaymentTypeId { get; }

    [JsonPropertyName("payment_type")] public string PaymentType { get; }

    [JsonPropertyName("transaction_type_id")]
    public int TransactionTypeId { get; }

    [JsonPropertyName("transaction_type")] public string TransactionType { get; private set; }

    [JsonPropertyName("verified_by")] public string VerifiedBy { get; }

    [JsonPropertyName("status_id")] public int PaymentStatusId { get; }

    [JsonPropertyName("status")] public string PaymentStatus { get; }

    [JsonPropertyName("payment_method")] public string PaymentMethod { get; }

    [JsonPropertyName("amount")] public decimal AmountPaid { get; }

    [JsonPropertyName("remarks")] public string Remarks { get; }

    [JsonPropertyName("correlation_id")] public string CorrelationId { get; }

    [JsonPropertyName("order_id")] public int OrderId { get; }
}