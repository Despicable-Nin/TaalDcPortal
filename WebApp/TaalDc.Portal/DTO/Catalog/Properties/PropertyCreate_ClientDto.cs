using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog
{
    public class PropertyCreate_ClientDto
    {
        public PropertyCreate_ClientDto(int projectId, int? propertyId, string name, double landArea)
        {
            ProjectId = projectId;
            PropertyId = propertyId;
            Name = name;
            LandArea = landArea;
        }

        public PropertyCreate_ClientDto()
        {
        }

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
