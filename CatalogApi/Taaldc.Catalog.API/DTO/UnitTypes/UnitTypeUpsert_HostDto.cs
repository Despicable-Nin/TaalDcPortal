﻿using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Taaldc.Catalog.API.DTO;

public class UnitTypeUpsert_HostDto
{
    [JsonPropertyName("unit_type_id")]
    [JsonProperty(Required = Required.Default)]
    public int? UnitTypeId { get; set; }

    [JsonPropertyName("unit_type_name")]
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; }

    [JsonPropertyName("unit_type_short_code")]
    [JsonProperty(Required = Required.Always)]
    public string ShortCode { get; set; }
}