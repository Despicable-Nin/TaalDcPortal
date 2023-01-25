using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TaalDc.Portal.ViewModels.Catalog;

public class UnitStatusUpdate_ClientDto
{
    public UnitStatusUpdate_ClientDto(int unitId, int status, string remarks)
    {
        UnitId = unitId;
        UnitStatus = status;
        Remarks = remarks;
    }

    [JsonPropertyName("unit_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitId { get; set; }

    [JsonPropertyName("unit_status_id")]
    [JsonProperty(Required = Required.Always)]
    public int UnitStatus { get; set; }

    [JsonPropertyName("remarks")]
    [JsonProperty(Required = Required.Always)]
    public string Remarks { get; set; }
}