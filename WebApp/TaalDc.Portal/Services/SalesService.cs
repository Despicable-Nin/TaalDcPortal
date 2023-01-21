using Microsoft.Extensions.Options;
using SeedWork;
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

	public Task<IEnumerable<Unit_Order_DTO>> GetUnitAndOrdersAvailability()
    {
        throw new NotImplementedException();
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