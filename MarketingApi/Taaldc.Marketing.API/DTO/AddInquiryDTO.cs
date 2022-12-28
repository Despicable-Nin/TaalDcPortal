using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Marketing.API.DTO;

public class AddInquiryDto
{
    [JsonPropertyName("inquiry_type")]
    [JsonProperty(Required = Required.Always)]
    public string InquiryType { get; set; }
    
    [JsonPropertyName("message")]
    [JsonProperty(Required = Required.Always)]
    public string Message { get; set; } 
    
    [JsonPropertyName("property_id")]
    [JsonProperty(Required = Required.Always)]
    public int PropertyId { get; set; }
    
    [JsonPropertyName("property")]
    [JsonProperty(Required = Required.Always)]
    public string Property { get; set; }
    
    [JsonPropertyName("remarks")]
    [JsonProperty(Required = Required.Always)]
    public string Remarks { get; set; } 
    
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
}