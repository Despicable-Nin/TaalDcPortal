using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.Units
{
	public class UnitTypeAvailabilityQueryResult
	{
		[JsonPropertyName("tower_id")]
		public int TowerId { get; set; }

		[JsonPropertyName("unit_type_id")]
		public int UnitTypeId { get; set; }

		[JsonPropertyName("unit_type_code")]
		public string UnitTypeCode { get; set; }

		[JsonPropertyName("unit_type")]
		public string UnitType { get; set; }

		[JsonPropertyName("min_floor_area")]
		public double MinFloorArea { get; set; }

		[JsonPropertyName("max_floor_area")]
		public double MaxFloorArea { get; set; }

		[JsonPropertyName("min_price")]
		public double MinPrice { get; set; }

		[JsonPropertyName("max_price")]
		public double MaxPrice { get; set; }

		[JsonPropertyName("available")]
		public int Available { get; set; }
	}
}
