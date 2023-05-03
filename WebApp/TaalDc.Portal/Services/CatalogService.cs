using System.Text.Json;
using Microsoft.Extensions.Options;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.DTO.Catalog.Floors;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MarketingService> _logger;
    private readonly string _remoteServiceUrl;
    private readonly IOptions<AppSettings> _settings;

    public CatalogService(
        IOptions<AppSettings> settings,
        HttpClient httpClient,
        ILogger<MarketingService> logger)
    {
        _settings = settings;
        _httpClient = httpClient;
        _logger = logger;
        _remoteServiceUrl = settings.Value.CatalogUrl;
    }

    public async Task<PaginationQueryResult<Floor_ClientDto>> GetFloors(string filter, string sortBy,
        SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        var uri = API.Catalog.GetFloors(_remoteServiceUrl);

        uri =
            $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<Floor_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<PaginationQueryResult<Property_ClientDto>> GetProperties(string filter, string sortBy,
        SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        var uri = API.Catalog.GetProperties(_remoteServiceUrl);

        uri =
            $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<Property_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<Property_ClientDto> GetPropertyById(int id)
    {
        var uri = API.Catalog.GetProperties(_remoteServiceUrl);

        uri = uri + $"/{id}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<Property_ClientDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<ActiveFloor_ClientDto>> GetActiveFloorsByTowerId(int towerId)
    {
        var uri = API.Catalog.GetActiveFloorByTowerId(_remoteServiceUrl, towerId);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<ActiveFloor_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<Floor_ClientDto> GetFloorById(int id)
    {
        var uri = API.Catalog.GetFloorById(_remoteServiceUrl, id);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<Floor_ClientDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<PaginationQueryResult<Tower_ClientDto>> GetTowers(string filter, string sortBy,
        SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        var uri = API.Catalog.GetTowers(_remoteServiceUrl);

        uri =
            $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<Tower_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }


    public async Task<Tower_ClientDto> GetTowerById(int id)
    {
        var uri = API.Catalog.GetTowers(_remoteServiceUrl);

        uri = uri + $"/{id}";

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<Tower_ClientDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<FloorUnitAvailability_ClientDto> GetFloorUnitsStatus(int id)
    {
        var uri = API.Catalog.GetUnitsColorScheme(_remoteServiceUrl, id);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<FloorUnitAvailability_ClientDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<PaginationQueryResult<Unit_ClientDto>> GetUnits(string filter, int? floorId, int? unitTypeId,
        int? viewId, int? statusId, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        var uri = API.Catalog.GetUnits(_remoteServiceUrl);

        uri = $"{uri}?filter={filter}&" +
              $"sortBy={sortBy}&" +
              $"sortOrder={sortOrder}&" +
              $"pageNumber={pageNumber}&" +
              $"pageSize={pageSize}";

        uri = floorId.HasValue ? $"{uri}&floorid={floorId}" : uri;

        uri = unitTypeId.HasValue ? $"{uri}&unitTypeId={unitTypeId}" : uri;

        uri = viewId.HasValue ? $"{uri}&viewId={viewId}" : uri;

        uri = statusId.HasValue ? $"{uri}&statusId={statusId}" : uri;

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<PaginationQueryResult<Unit_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<Unit_ClientDto> GetUnitById(int id)
    {
        var uri = API.Catalog.GetUnitById(_remoteServiceUrl, id);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<Unit_ClientDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<IEnumerable<UnitType_ClientDto>> GetUnitTypes()
    {
        var uri = API.Catalog.GetUnitTypes(_remoteServiceUrl);

        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<IEnumerable<UnitType_ClientDto>>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<Response> CreateProperty(PropertyCreate_ClientDto model)
    {
        //we are not expecting any new project..
        if (model.ProjectId == 0) model.ProjectId = 1;

        var uri = API.Catalog.AddProperty(_remoteServiceUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Response> CreateTower(TowerCreate_ClientDto model)
    {
        var uri = API.Catalog.AddTower(_remoteServiceUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Response> CreateFloor(FloorCreate_ClientDto model)
    {
        var uri = API.Catalog.AddFloor(_remoteServiceUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Response>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }


    public async Task<Response> CreateUnit(UnitCreate_ClientDto model)
    {
        var uri = API.Catalog.AddUnit(_remoteServiceUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    /// <summary>
    ///     Updates unit but only for available and blocked only.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<Response> UpdateUnit(UnitUpdate_ClientDto model)
    {
        var uri = API.Catalog.EditUnit(_remoteServiceUrl, model.UnitId);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Response> CreateUnitType(UnitTypeCreate_ClientDto model)
    {
        var uri = API.Catalog.AddUnitType(_remoteServiceUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Response> UpdateUnitStatus(UnitStatusUpdate_ClientDto model)
    {
        var uri = API.Catalog.UpdateUnitStatus(_remoteServiceUrl);

        var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode) { 
            return JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return new Response("Unit status cannot be updated", false, model.UnitId);
    }

    
}