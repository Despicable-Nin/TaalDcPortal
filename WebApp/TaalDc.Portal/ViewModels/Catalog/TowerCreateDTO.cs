using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog
{
    public class TowerCreateDTO
    {
        public TowerCreateDTO(int? towerId, int propertyId, string name, string address)
        {
            TowerId = towerId;
            PropertyId = propertyId;
            Name = name;
            Address = address;
        }

        public TowerCreateDTO()
        {
        }

        [JsonPropertyName("property_id")]
        public int PropertyId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("tower_id")]
        public int? TowerId { get; set; }
    }
}
