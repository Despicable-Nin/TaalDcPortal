using System.Drawing.Printing;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.DTO.Sales.Buyer;
using TaalDc.Portal.DTO.Sales.Contracts;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Models;

namespace TaalDc.Portal.Services;

public class SalesService : ISalesService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SalesService> _logger;
    private readonly string _remoteServiceBaseUrl;
    private readonly IOptions<AppSettings> _settings;

    public SalesService(IOptions<AppSettings> settings,
        HttpClient httpClient,
        ILogger<SalesService> logger,
        ICatalogService catalogService)
    {
        _settings = settings;
        _httpClient = httpClient;
        _logger = logger;

        _remoteServiceBaseUrl = $"{settings.Value.SalesUrl}";
    }


    //DASHBOARD
    public async Task<IEnumerable<GetResidentialAvailabilityByTypeResponse>> GetResidentialAvailabilityByType()
    {
        var uri = API.Sales.GetAvailabilityPerResidentialUnitType(_remoteServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<GetResidentialAvailabilityByTypeResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<GetResidentialAvailabilityByViewResponse>> GetResidentialAvailabilityByView()
    {
        var uri = API.Sales.GetAvailabilityOfResidentialUnitPerView(_remoteServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<GetResidentialAvailabilityByViewResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<GetResidentialUnitsCountByStatusResponse>> GetResidentialUnitsCountByStatus()
    {
        var uri = API.Sales.GetUnitCountSummaryByStatus(_remoteServiceBaseUrl);
        
        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<GetResidentialUnitsCountByStatusResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }


    public async Task<IEnumerable<GetResidentialUnitsCountByStatusResponse>> GetParkingUnitsCountByStatus()
    {
        var uri = API.Sales.GetParkingCountSummaryByStatus(_remoteServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<GetResidentialUnitsCountByStatusResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<GetAvailabilityPerParkingUnitTypeResponse>> GetAvailabilityPerParkingUnitType()
    {
        var uri = API.Sales.GetAvailabilityPerParkingUnitType(_remoteServiceBaseUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<GetAvailabilityPerParkingUnitTypeResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<GetParkingUnitTypeAvailabilityPerFloorResponse>> GetParkingUnitTypeAvailabilityPerFloor()
    {
        var parkings = new List<GetParkingUnitTypeAvailabilityPerFloorResponse>();

        try { 
            var uri = API.Sales.GetParkingUnitTypeAvailabilityPerFloor(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            parkings = JsonSerializer.Deserialize<List<GetParkingUnitTypeAvailabilityPerFloorResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }catch{
            throw;
        }

        return parkings.ToList();
    }



    public async Task<Response> AcceptPayment(int orderId, int paymentId)
    {
        var uri = API.Sales.AcceptPayment(_remoteServiceBaseUrl, orderId, paymentId);

        var response = await _httpClient.PostAsync(uri, null);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Payment cannot be updated.");
    }

    public async Task<Response> VoidPayment(int orderId, int paymentId)
    {
        var uri = API.Sales.VoidPayment(_remoteServiceBaseUrl, orderId, paymentId);

        var response = await _httpClient.PostAsync(uri, null);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Payment cannot be updated.");
    }

    public async Task<Response> AddPayment(AddPaymentRequest model)
    {
        var uri = API.Sales.AddPayment(_remoteServiceBaseUrl, model.TransactionId);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Sale cannot be created.");
    }

  

    public async Task<GetSalesByIdResponse> GetSalesById(int id)
    {
        var uri = API.Sales.GetSales(_remoteServiceBaseUrl);
        uri = $"{uri}/{id}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<GetSalesByIdResponse>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<GetSalesPaymentResponse>> GetSalesPayments(int id)
    {
        var uri = API.Sales.GetSales(_remoteServiceBaseUrl);
        uri = $"{uri}/{id}/payments";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<GetSalesPaymentResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<PaginationQueryResult<GetSalesByIdResponse>> GetUnitAndOrdersAvailability(int unitStatusId,
        int pageNumber, int pageSize, int? floorId, int? unitTypeId, int? viewId, string? filter, string broker = "")
    {
        var uri = API.Sales.GetSales(_remoteServiceBaseUrl);

        var brokerString = string.IsNullOrEmpty(broker) ? string.Empty : $"&broker={broker}";
        var filterString = string.IsNullOrEmpty(filter) ? string.Empty : $"&filter={filter}";

        uri =
            $"{uri}?floorId={floorId}&unitTypeId={unitTypeId}&viewId={viewId}&unitStatus={unitStatusId}&pageNumber={pageNumber}&pageSize={pageSize}{brokerString}{filterString}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<GetSalesByIdResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<AddBuyerOrderResponse> AddBuyerOrder(AddBuyerOrderRequest model)
    {
        var uri = API.Sales.SellUnit(_remoteServiceBaseUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<AddBuyerOrderResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Sale cannot be created.");
    }

    public async Task<Response> AddBuyer(AddBuyerRequest model)
    {
        var uri = API.Sales.AddBuyer(_remoteServiceBaseUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be created.");
    }

    public async Task<Response> UpdateBuyerInfo(UpdateBuyerInfoRquest model)
    {
        var uri = API.Sales.UpdateBuyerInfo(_remoteServiceBaseUrl, model.BuyerId);

        var response = await _httpClient.PutAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be updated.");
    }

    public async Task<bool> UpdateBuyerContact(UpdateBuyerContactRequest model)
    {
        var uri = API.Sales.UpdateBuyerContact(_remoteServiceBaseUrl, model.BuyerId);

        var response = await _httpClient.PutAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<bool>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be updated.");
    }

    public async Task<Response> UpdateBuyerMisc(UpdateBuyerMiscRequest model)
    {
        var uri = API.Sales.UpdateBuyerMisc(_remoteServiceBaseUrl, model.BuyerId);

        var response = await _httpClient.PutAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be updated.");
    }

    public async Task<Response> PatchBuyerAddress(PatchBuyerAddressRequest model)
    {
        var uri = API.Sales.UpdateBuyerAddress(_remoteServiceBaseUrl, model.BuyerId);

        var json = JsonSerializer.Serialize(model);

        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync(uri, jsonContent);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be updated.");
    }

    public async Task<Response> UpdateBuyerCompany(UpdateBuyerCompanyRequest model)
    {
        var uri = API.Sales.UpdateBuyerCompany(_remoteServiceBaseUrl, model.BuyerId);

        var response = await _httpClient.PutAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be updated.");
    }
    
    public async Task<Response> UpsertSpouse(UpsertBuyerSpouseRequest model)
    {
        var uri = API.Sales.UpsertSpouse(_remoteServiceBaseUrl, model.BuyerId);

        var response = await _httpClient.PutAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Buyer cannot be updated.");
    }

    public async Task<PaginationQueryResult<GetBuyerResponse>> GetBuyers(int pageNumber, int pageSize, string name, string email)
    {
        var uri = API.Sales.GetBuyers(_remoteServiceBaseUrl);

        uri =
            $"{uri}?pageNumber={pageNumber}&pageSize={pageSize}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<GetBuyerResponse>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<GetBuyerResponse> GetBuyer(int buyerId)
    {
        var uri = API.Sales.GetBuyerByID(_remoteServiceBaseUrl, buyerId);
       
        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<GetBuyerResponse>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<Response> CreateContract(CreateContractRequest model)
    {
        var uri = API.Sales.SellUnit(_remoteServiceBaseUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        throw new Exception("Sale cannot be created.");
    }

    public async Task<IEnumerable<ContractOrderItem_ClientDto>> GetContractOrderItems(int id)
    {
        var uri = API.Sales.GetSales(_remoteServiceBaseUrl);
        uri = $"{uri}/{id}/units";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<ContractOrderItem_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<Contract_ClientDto>> GetBuyerContracts(int id)
    {
        var uri = API.Sales.GetSales(_remoteServiceBaseUrl);
        uri = $"{uri}/{id}/buyer-contract";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<Contract_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }
}