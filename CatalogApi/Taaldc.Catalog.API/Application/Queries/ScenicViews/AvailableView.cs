using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.Application.Queries.ScenicViews
{
	public class AvailableView
	{
		[JsonPropertyName("view_id")]
		public int ViewId { get; set; }

		[JsonPropertyName("scenic_view")]
		public string ScenicView { get; set; }
	}
}
