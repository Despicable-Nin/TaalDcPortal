using System.Text.Json.Serialization;

namespace Taaldc.Catalog.API.DTO;

public class ProjectUpsert_HostDto
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("developer")] public string Developer { get; set; }
    [JsonPropertyName("project_id")] public int? ProjectId { get; set; }
}