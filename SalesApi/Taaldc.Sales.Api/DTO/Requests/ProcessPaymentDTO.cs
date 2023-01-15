using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Sales.Api.DTO;

public class ProcessPaymentDTO
{
    [JsonPropertyName("verified_by")]
    [JsonProperty(Required = Required.Always)]
    public string VerifiedBy { get; set; }
    
    [JsonPropertyName("payment_id")]
    [JsonProperty(Required = Required.Always)]
    public int PaymentId { get; set; }
    
    [JsonPropertyName("unit_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitId { get; set; }
}