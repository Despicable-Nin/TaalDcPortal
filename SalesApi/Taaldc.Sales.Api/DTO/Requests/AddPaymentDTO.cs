using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Sales.Api.DTO;

public class AddPaymentDTO
{
    [JsonPropertyName("transaction_id")]
    [JsonProperty(Required = Required.Always)]
    public int TransactionId { get; set; }

    [JsonPropertyName("payment_method")]
    [JsonProperty(Required = Required.Always)]
    public string PaymentMethod { get; set; }

    [JsonPropertyName("amount")]
    [JsonProperty(Required = Required.Always)]
    public decimal AmountPaid { get; set; }

    [JsonPropertyName("remarks")]
    [JsonProperty(Required = Required.AllowNull)]
    public string Remarks { get; set; }

    [JsonPropertyName("confirmation_number")]
    [JsonProperty(Required = Required.Always)]
    public string ConfirmationNumber { get; set; }

    [JsonPropertyName("transaction_type_id")]
    [JsonProperty(Required = Required.Always)]
    public int TransactionTypeId { get; set; }

    [JsonPropertyName("payment_type_id")]
    [JsonProperty(Required = Required.Always)]
    public int PaymentTypeId { get; set; }

    [JsonPropertyName("payment_date")]
    [JsonProperty(Required = Required.Always)]
    public DateTime PaymentDate { get; set; }

    [JsonPropertyName("correlation_id")]
    [JsonProperty(Required = Required.AllowNull)]
    public string CorrelationId { get; set; }
}