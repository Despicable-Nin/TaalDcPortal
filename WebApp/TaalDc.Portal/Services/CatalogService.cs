using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MarketingService> _logger;

        private readonly string _removeServiceBaseUrl;

        public CatalogService(
            IOptions<AppSettings> settings, 
            HttpClient httpClient, 
            ILogger<MarketingService> logger)
        {
            _settings = settings;
            _httpClient = httpClient;
            _logger = logger;

            _removeServiceBaseUrl = $"{settings.Value.CatalogUrl}";
        }

        public async Task<PaginationQueryResult<FloorDTO>> GetFloors(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            var uri = API.Catalog.GetFloors(_removeServiceBaseUrl);

            uri = $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonSerializer.Deserialize<PaginationQueryResult<FloorDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<PaginationQueryResult<PropertyDTO>> GetProperties(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            var uri = API.Catalog.GetProperties(_removeServiceBaseUrl);

            uri = $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonSerializer.Deserialize<PaginationQueryResult<PropertyDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<PropertyDTO> GetPropertyById(int id)
        {
            var uri = API.Catalog.GetProperties(_removeServiceBaseUrl);

            uri = uri + $"/{id}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonSerializer.Deserialize<PropertyDTO>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<PaginationQueryResult<TowerDTO>> GetTowers(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            var uri = API.Catalog.GetTowers(_removeServiceBaseUrl);

            uri = $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonSerializer.Deserialize<PaginationQueryResult<TowerDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<PaginationQueryResult<UnitDTO>> GetUnits(string filter, int? floorId, int? unitTypeId, int? viewId, int? statusId, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            var uri = API.Catalog.GetUnits(_removeServiceBaseUrl);

            uri = $"{uri}?filter={filter}&" +
                $"sortBy={sortBy}&" +
                $"sortOrder={sortOrder}&" +
                $"pageNumber={pageNumber}&" +
                $"pageSize={pageSize}";


            if (floorId.HasValue)
            {
                uri = uri + $"&floorId={floorId}";
            }

            if (unitTypeId.HasValue)
            {
                uri = uri + $"&unitTypeId={unitTypeId}";
            }

            if (viewId.HasValue)
            {
                uri = uri + $"&viewId={viewId}";
            }

            if (statusId.HasValue)
            {
                uri = uri + $"&statusId={statusId}";
            }

            try { 
                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<PaginationQueryResult<UnitDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }catch(Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<IEnumerable<UnitTypeDTO>> GetUnitTypes()
        {
            var uri = API.Catalog.GetUnitTypes(_removeServiceBaseUrl);

            try
            {
                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<IEnumerable<UnitTypeDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }




        public async Task<CommandResult> CreateProperty(PropertyCreateDTO model)
        {
            if (model.ProjectId == 0) model.ProjectId = 1;

            var uri = API.Catalog.AddProperty(_removeServiceBaseUrl);

            var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CommandResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Property cannot be created.");
        }

        public async Task<CommandResult> CreateTower(TowerCreateDTO model)
        {
            var uri = API.Catalog.AddTower(_removeServiceBaseUrl);

            var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CommandResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Tower cannot be created.");
        }

        public async Task<CommandResult> CreateFloor(FloorCreateDTO model)
        {
            var uri = API.Catalog.AddFloor(_removeServiceBaseUrl);

            var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CommandResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Floor cannot be created.");
        }


        public async Task<CommandResult> CreateUnit(UnitCreateDTO model)
        {
            var uri = API.Catalog.AddUnit(_removeServiceBaseUrl);

            var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CommandResult>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Unit cannot be created.");
        }

        
    }
}
