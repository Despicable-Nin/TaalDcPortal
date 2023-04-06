using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.DTO.Catalog.Floors;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Services;

public interface ICatalogService
{
    Task<PaginationQueryResult<Property_ClientDto>> GetProperties(string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10);

    Task<Property_ClientDto> GetPropertyById(int id);

    Task<PaginationQueryResult<Tower_ClientDto>> GetTowers(string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10);

    Task<Tower_ClientDto> GetTowerById(int id);

    Task<PaginationQueryResult<Floor_ClientDto>> GetFloors(string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10);

    Task<IEnumerable<ActiveFloor_ClientDto>> GetActiveFloorsByTowerId(int towerId);

    Task<Floor_ClientDto> GetFloorById(int id);

    Task<FloorUnitAvailability_ClientDto> GetFloorUnitsStatus(int id);
    Task<PaginationQueryResult<Unit_ClientDto>> GetUnits(string filter,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int? statusId,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10);

    Task<Unit_ClientDto> GetUnitById(int id);


    Task<IEnumerable<UnitType_ClientDto>> GetUnitTypes();
    

    Task<Response> CreateProperty(PropertyCreate_ClientDto model);
    Task<Response> CreateTower(TowerCreate_ClientDto model);
    Task<Response> CreateFloor(FloorCreate_ClientDto model);
    Task<Response> CreateUnit(UnitCreate_ClientDto model);
    Task<Response> UpdateUnit(UnitUpdate_ClientDto model);
    Task<Response> CreateUnitType(UnitTypeCreate_ClientDto model);
    Task<Response> UpdateUnitStatus(UnitStatusUpdate_ClientDto model);
}