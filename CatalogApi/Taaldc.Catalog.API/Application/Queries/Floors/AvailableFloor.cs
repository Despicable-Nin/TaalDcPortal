using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.Floors
{
	public class AvailableFloor
	{
		[JsonPropertyName("floor_id")]
		public int FloorId { get; set; }

		[JsonPropertyName("floor_name")]
		public string FloorName { get; set; }
		[JsonPropertyName("floor_plan_file_path")]
		public string FloorPlanFilePath { get; set; }
	}
}
