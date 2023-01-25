using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.UnitTypes
{
    public class UnitTypeQueryResult
    {
        [JsonPropertyName("unit_type_id")]
        public int Id { get; set; }

        [JsonPropertyName("unit_type_name")]
        public string Name { get; set; }

        [JsonPropertyName("unit_type_short_code")]
        public string ShortCode { get; set; }
    }
}
