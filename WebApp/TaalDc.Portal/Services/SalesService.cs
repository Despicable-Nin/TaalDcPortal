using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SeedWork;
using System.Globalization;
using System.Text.Json;
using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Catalog;
using TaalDc.Portal.ViewModels.Sales;

namespace TaalDc.Portal.Services;

public class SalesService : ISalesService
{
	private readonly IOptions<AppSettings> _settings;
	private readonly HttpClient _httpClient;
	private readonly ILogger<SalesService> _logger;
	private readonly string _removeServiceBaseUrl;

	public SalesService(IOptions<AppSettings> settings,
			HttpClient httpClient,
			ILogger<SalesService> logger,
			ICatalogService catalogService)
	{
		_settings = settings;
		_httpClient = httpClient;
		_logger = logger;

		_removeServiceBaseUrl = $"{settings.Value.SalesUrl}";
	}

	public async Task<PaginationQueryResult<Unit_Order_DTO>> GetUnitAndOrdersAvailability(int unitStatusId, int pageNumber, int pageSize, int? floorId, int? unitTypeId, int? viewId)
	{
		var uri = API.Sales.GetUnitAndOrdersAvailability(_removeServiceBaseUrl);
		uri = $"{uri}?floorId={floorId}&unitTypeId={unitTypeId}&viewId={viewId}&unitStatus={unitStatusId}&pageNumber={pageNumber}&pageSize={pageSize}";

		var responseString = await _httpClient.GetStringAsync(uri);

		var result = JsonSerializer.Deserialize<PaginationQueryResult<Unit_Order_DTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

		return result;
	}

	public async Task<SellUnitCommandResult> SellUnit(SalesCreateDTO model)
	{
		var uri = API.Sales.SellUnit(_removeServiceBaseUrl);

		var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

		var content = await response.Content.ReadAsStringAsync();

		if (response.IsSuccessStatusCode)
		{
			return JsonSerializer.Deserialize<SellUnitCommandResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}

		throw new Exception("Sale cannot be created.");
	}
}