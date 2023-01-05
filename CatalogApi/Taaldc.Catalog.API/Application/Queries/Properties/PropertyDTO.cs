using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Text.Json.Serialization;
using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Properties
{
    public class PropertyDTO
    {
        [JsonPropertyName("property_id")]
        public int Id { get; set; }

        [JsonPropertyName("property_name")]
        public string PropertyName { get; set; }

        [JsonPropertyName("land_area")]
        public double LandArea { get; set; }

        [JsonPropertyName("towers")]
        public int Towers { get; set; }
    }
}
