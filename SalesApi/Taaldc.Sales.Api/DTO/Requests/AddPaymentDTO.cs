using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Sales.Api.DTO;

public class AddPaymentDTO
{
    [JsonPropertyName("transaction_id")]
    [JsonProperty(Required = Required.Always)]
    public int TransactionId { get; private set; }
    
    [JsonPropertyName("payment_method")]
    [JsonProperty(Required = Required.Always)]
    public string PaymentMethod { get; private set; }
    
    [JsonPropertyName("amount")]
    [JsonProperty(Required = Required.Always)]
    public decimal AmountPaid { get; private set; }
    
    [JsonPropertyName("remarks")]
    [JsonProperty(Required = Required.AllowNull)]
    public string Remarks { get; private set; }
    
    [JsonPropertyName("confirmation_number")]
    [JsonProperty(Required = Required.Always)]
    public string ConfirmationNumber { get; private set; }
    
    [JsonPropertyName("transaction_type_id")]
    [JsonProperty(Required = Required.Always)]
    public int TransactionTypeId { get; private set; }
    
    [JsonPropertyName("payment_type_id")]
    [JsonProperty(Required = Required.Always)]
    public int PaymentTypeId { get; private set; }
    
    [JsonPropertyName("payment_date")]
    [JsonProperty(Required = Required.Always)]
    public DateTime PaymentDate { get; private set; }
    
    [JsonPropertyName("correlation_id")]
    [JsonProperty(Required = Required.AllowNull)]
    public string CorrelationId { get; private set; }
}