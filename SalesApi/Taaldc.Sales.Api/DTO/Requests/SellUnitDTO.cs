using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Sales.Api.DTO;

public class SellUnitDTO
{
    [JsonPropertyName("code")]
    [JsonProperty(Required = Required.AllowNull)]
    public string Code { get; set; }
    
    [JsonPropertyName("broker")]
    [JsonProperty(Required = Required.Always)]
    public string Broker { get; set; }
    
    [JsonPropertyName("is_refundable")]
    [JsonProperty(Required = Required.Always)]
    public bool IsRefundable { get; set; }
    
    [JsonPropertyName("unit_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitId { get; set; }
    
    [JsonPropertyName("selling_price")]
    [JsonProperty(Required = Required.Always)]
    public decimal SellingPrice { get; set; }

    [JsonPropertyName("salutation")]
    [JsonProperty(Required = Required.Always)]
    public string Salutation { get;  set; }
    
    [JsonPropertyName("first_name")]
    [JsonProperty(Required = Required.Always)]
    public string FirstName { get;  set; }
    
    [JsonPropertyName("last_name")]
    [JsonProperty(Required = Required.Always)]
    public string LastName { get;  set; }
    
    [JsonPropertyName("email_address")]
    [JsonProperty(Required = Required.Always)]
    [EmailAddress]
    public string EmailAddress { get;  set; }
    
    [JsonPropertyName("contact_number")]
    [JsonProperty(Required = Required.Always)]
    public string ContactNo { get;  set; }
    
    [JsonPropertyName("country")]
    [JsonProperty(Required = Required.Default)]
    public string Country { get;  set; }
    
    [JsonPropertyName("province")]
    [JsonProperty(Required = Required.Default)]
    public string Province { get;  set; }
    
    [JsonPropertyName("city")]
    [JsonProperty(Required = Required.Default)]
    public string TownCity { get;  set; }
    
    [JsonPropertyName("zipcode")]
    [JsonProperty(Required = Required.Always)]
    public string ZipCode { get; set; }
    
    [JsonPropertyName("reservation_fee")]
    [JsonProperty(Required = Required.Always)]
    public decimal Reservation { get; set; }

    [JsonPropertyName("downpayment")]
    [JsonProperty(Required = Required.AllowNull)]
    public decimal DownPayment { get; set; } 
    
    [JsonPropertyName("payment_date")]
    [JsonProperty(Required = Required.Always)]
    public DateTime PaymentDate { get; set; }
    
    [JsonPropertyName("payment_method")]
    [JsonProperty(Required = Required.Always)]
    public string PaymentMethod { get; set; }
    
    [JsonPropertyName("remarks")]
    [JsonProperty(Required = Required.AllowNull)]
    public string Remarks { get; set; }
}