using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using SeedWork;
using System.Drawing.Printing;
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

    public async Task<Unit_Order_DTO> GetSalesById(int id)
	{
		var uri = API.Sales.GetSales(_removeServiceBaseUrl);
		uri = $"{uri}/{id}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<Unit_Order_DTO>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<PaymentDTO>> GetSalesPayments(int id)
    {
        var uri = API.Sales.GetSales(_removeServiceBaseUrl);
        uri = $"{uri}/{id}/payments";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<PaymentDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<PaginationQueryResult<Unit_Order_DTO>> GetUnitAndOrdersAvailability(int unitStatusId, int pageNumber, int pageSize, int? floorId, int? unitTypeId, int? viewId, string broker = "")
	{
		var uri = API.Sales.GetSales(_removeServiceBaseUrl);

		var brokerString = string.IsNullOrEmpty(broker) ? string.Empty : $"&broker={broker}";
		uri = $"{uri}?floorId={floorId}&unitTypeId={unitTypeId}&viewId={viewId}&unitStatus={unitStatusId}&pageNumber={pageNumber}&pageSize={pageSize}{brokerString}";

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