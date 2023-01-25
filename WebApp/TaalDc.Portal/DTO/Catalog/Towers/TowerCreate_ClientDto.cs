using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TaalDc.Portal.ViewModels.Catalog
{
    public class TowerCreate_ClientDto
    {
        public TowerCreate_ClientDto(int? towerId, int propertyId, string name, string address)
        {
            TowerId = towerId;
            PropertyId = propertyId;
            Name = name;
            Address = address;
        }

        public TowerCreate_ClientDto()
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
