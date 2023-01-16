using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog
{
    public class FloorCreateDTO
    {
        public FloorCreateDTO(int towerId, int? floorId, string name, string description, string floorPlanFilePath)
        {
            TowerId = towerId;
            FloorId = floorId;
            Name = name;
            Description = description;
            FloorPlanFilePath = floorPlanFilePath;
        }

        [JsonPropertyName("tower_id")]
        public int TowerId { get; set; }

        [JsonPropertyName("floor_id")]
        public int? FloorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("project_id")]
        public string FloorPlanFilePath { get; set; }
    }
}
