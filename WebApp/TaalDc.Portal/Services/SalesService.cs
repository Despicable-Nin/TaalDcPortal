﻿using System.Text.Json;
using Microsoft.Extensions.Options;
using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Sales;

namespace TaalDc.Portal.Services;

public class SalesService : ISalesService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SalesService> _logger;
    private readonly string _removeServiceBaseUrl;
    private readonly IOptions<AppSettings> _settings;

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


    //DASHBOARD
    public async Task<IEnumerable<AvailabilityByUnitType_ClientDto>> GetResidentialAvailabilityByType()
    {
        var uri = API.Sales.GetAvailabilityPerResidentialUnitType(_removeServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<AvailabilityByUnitType_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<AvailabilityByView_ClientDto>> GetResidentialAvailabilityByView()
    {
        var uri = API.Sales.GetAvailabilityOfResidentialUnitPerView(_removeServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<AvailabilityByView_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<UnitCountByStatus_ClientDto>> GetResidentialUnitsCountByStatus()
    {
        var uri = API.Sales.GetUnitCountSummaryByStatus(_removeServiceBaseUrl);
        
        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<UnitCountByStatus_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }


    public async Task<IEnumerable<UnitCountByStatus_ClientDto>> GetParkingUnitsCountByStatus()
    {
        var uri = API.Sales.GetParkingCountSummaryByStatus(_removeServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<UnitCountByStatus_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<ParkingUnitAvailabilityPerUnitType_ClientDto>> GetAvailabilityPerParkingUnitType()
    {
        var uri = API.Sales.GetAvailabilityPerParkingUnitType(_removeServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<ParkingUnitAvailabilityPerUnitType_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<ParkingUnitAvailabilityPerFloor_ClientDto>> GetParkingUnitTypeAvailabilityPerFloor()
    {
        var uri = API.Sales.GetParkingUnitTypeAvailabilityPerFloor(_removeServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<ParkingUnitAvailabilityPerFloor_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }



    public async Task<CommandResult> AcceptPayment(int orderId, int paymentId)
    {
        var uri = API.Sales.AcceptPayment(_removeServiceBaseUrl, orderId, paymentId);

        var response = await _httpClient.PostAsync(uri, null);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<CommandResult>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Payment cannot be updated.");
    }

    public async Task<CommandResult> AddPayment(PaymentCreate_ClientDto model)
    {
        var uri = API.Sales.AddPayment(_removeServiceBaseUrl, model.TransactionId);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<CommandResult>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Sale cannot be created.");
    }

  

    public async Task<OrderUnitBuyer_ClientDto> GetSalesById(int id)
    {
        var uri = API.Sales.GetSales(_removeServiceBaseUrl);
        uri = $"{uri}/{id}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<OrderUnitBuyer_ClientDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<Payment_ClientDto>> GetSalesPayments(int id)
    {
        var uri = API.Sales.GetSales(_removeServiceBaseUrl);
        uri = $"{uri}/{id}/payments";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<Payment_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<PaginationQueryResult<OrderUnitBuyer_ClientDto>> GetUnitAndOrdersAvailability(int unitStatusId,
        int pageNumber, int pageSize, int? floorId, int? unitTypeId, int? viewId, string broker = "")
    {
        var uri = API.Sales.GetSales(_removeServiceBaseUrl);

        var brokerString = string.IsNullOrEmpty(broker) ? string.Empty : $"&broker={broker}";
        uri =
            $"{uri}?floorId={floorId}&unitTypeId={unitTypeId}&viewId={viewId}&unitStatus={unitStatusId}&pageNumber={pageNumber}&pageSize={pageSize}{brokerString}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<OrderUnitBuyer_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<SellUnitCommandResult> SellUnit(SalesCreate_ClientDto model)
    {
        var uri = API.Sales.SellUnit(_removeServiceBaseUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<SellUnitCommandResult>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Sale cannot be created.");
    }

  
}