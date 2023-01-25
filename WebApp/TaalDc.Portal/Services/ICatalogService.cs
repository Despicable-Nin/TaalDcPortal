using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Services
{
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

        Task<Floor_ClientDto> GetFloorById(int id);

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



        Task<CommandResult> CreateProperty(PropertyCreate_ClientDto model);
        Task<CommandResult> CreateTower(TowerCreate_ClientDto model);
        Task<CommandResult> CreateFloor(FloorCreate_ClientDto model);
        Task<CommandResult> CreateUnit(UnitCreate_ClientDto model);
        Task<CommandResult> UpdateUnit(UnitUpdate_ClientDto model);
        Task<CommandResult> CreateUnitType(UnitTypeCreate_ClientDto model);
		Task<CommandResult> UpdateUnitStatus(UnitStatusUpdate_ClientDto model);
	}
}
