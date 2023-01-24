using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using Microsoft.CodeAnalysis.Operations;
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
        private readonly string _remoteServiceUrl;

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

        public async Task<PaginationQueryResult<FloorDTO>> GetFloors(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var uri = API.Catalog.GetFloors(_remoteServiceUrl);

                uri =
                    $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<PaginationQueryResult<FloorDTO>>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PaginationQueryResult<PropertyDTO>> GetProperties(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var uri = API.Catalog.GetProperties(_remoteServiceUrl);

                uri =
                    $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<PaginationQueryResult<PropertyDTO>>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PropertyDTO> GetPropertyById(int id)
        {
            try
            {
                var uri = API.Catalog.GetProperties(_remoteServiceUrl);

                uri = uri + $"/{id}";

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<PropertyDTO>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FloorDTO> GetFloorById(int id)
        {
            try
            {
                var uri = API.Catalog.GetFloorById(_remoteServiceUrl, id);

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<FloorDTO>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PaginationQueryResult<TowerDTO>> GetTowers(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var uri = API.Catalog.GetTowers(_remoteServiceUrl);

                uri =
                    $"{uri}?filter={filter}&sortBy={sortBy}&sortOrder={sortOrder}&pageNumber={pageNumber}&pageSize={pageSize}";

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<PaginationQueryResult<TowerDTO>>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<TowerDTO> GetTowerById(int id)
        {
            try
            {
                var uri = API.Catalog.GetTowers(_remoteServiceUrl);

                uri = uri + $"/{id}";

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<TowerDTO>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PaginationQueryResult<UnitDTO>> GetUnits(string filter, int? floorId, int? unitTypeId, int? viewId, int? statusId, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            try
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

                var result = JsonSerializer.Deserialize<PaginationQueryResult<UnitDTO>>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public async Task<UnitDTO> GetUnitById(int id)
        {
            try
            {
                var uri = API.Catalog.GetUnitById(_remoteServiceUrl, id);

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<UnitDTO>(responseString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
    
        }
        
        public async Task<IEnumerable<UnitTypeDTO>> GetUnitTypes()
        {
           
            try
            {
                var uri = API.Catalog.GetUnitTypes(_remoteServiceUrl);

                var responseString = await _httpClient.GetStringAsync(uri);

                var result = JsonSerializer.Deserialize<IEnumerable<UnitTypeDTO>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return result;
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public async Task<CommandResult> CreateProperty(PropertyCreateDTO model)
        {
            try
            {
                //we are not expecting any new project..
                if (model.ProjectId == 0) model.ProjectId = 1;

                var uri = API.Catalog.AddProperty(_remoteServiceUrl);

                var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

                var content = await response.Content.ReadAsStringAsync();
                
                return JsonSerializer.Deserialize<CommandResult>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommandResult> CreateTower(TowerCreateDTO model)
        {
            try
            {
                var uri = API.Catalog.AddTower(_remoteServiceUrl);

                var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<CommandResult>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommandResult> CreateFloor(FloorCreateDTO model)
        {
            try
            {
                var uri = API.Catalog.AddFloor(_remoteServiceUrl);

                var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CommandResult>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<CommandResult> CreateUnit(UnitCreateDTO model)
        {
            try
            {
                var uri = API.Catalog.AddUnit(_remoteServiceUrl);

                var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<CommandResult>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommandResult> CreateUnitType(UnitTypeCreateDTO model)
        {
            try
            {
                var uri = API.Catalog.AddUnitType(_remoteServiceUrl);

                var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

                var content = await response.Content.ReadAsStringAsync();
                
                return JsonSerializer.Deserialize<CommandResult>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            }
            catch (Exception ex)
            {
                throw;
            }
        }

		public async Task<CommandResult> UpdateUnitStatus(UnitStatusUpdateDTO model)
		{
            try
            {
                var uri = API.Catalog.UpdateUnitStatus(_remoteServiceUrl);

                var response = await _httpClient.PostAsJsonAsync(uri, model, CancellationToken.None);

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<CommandResult>(content,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
          
            }
            catch (Exception ex)
            {
                throw;
            }
        }
	}
}
