﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog
{
    public class FloorCreate_ClientDto
    {
        public FloorCreate_ClientDto(int towerId, int? floorId, string name, string description, string floorPlanFilePath)
        {
            TowerId = towerId;
            FloorId = floorId;
            Name = name;
            Description = description;
            FloorPlanFilePath = floorPlanFilePath;
        }

        public FloorCreate_ClientDto()
        {
                
        }

        [JsonPropertyName("tower_id")]
        public int TowerId { get; set; }

        [JsonPropertyName("floor_id")]
        public int? FloorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("floor_plan_file_path")]
        public string FloorPlanFilePath { get; set; }
    }
}
