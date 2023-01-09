﻿using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog
{
    public class PropertyCreateDTO
    {
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }
        [JsonPropertyName("property_id")]
        public int? PropertyId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("land_area")]
        public double LandArea { get; set; }
    }
}
