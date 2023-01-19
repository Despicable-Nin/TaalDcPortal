using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Catalog.API.DTO;

public class ChangeStatusOfUnitDTO
{
    [JsonPropertyName("unit_id")] 
    [JsonProperty(Required = Required.Always)]
    public int UnitId { get; private set; }
    
    [JsonPropertyName("unit_status_id")] 
    [JsonProperty(Required = Required.Always)]
    public int UnitStatus { get; private set; }
    
    [JsonPropertyName("remarks")] 
    [JsonProperty(Required = Required.Always)]
    public string Remarks { get; private set; }
}