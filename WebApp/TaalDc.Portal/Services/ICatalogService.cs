﻿using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Services
{
    public interface ICatalogService
    {
        Task<PaginationQueryResult<PropertyDTO>> GetProperties(string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10);

        Task<PaginationQueryResult<TowerDTO>> GetTowers(string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10);

        Task<PaginationQueryResult<FloorDTO>> GetFloors(string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10);

        Task<PaginationQueryResult<UnitDTO>> GetUnits(string filter,
             int? floorId,
             int? unitTypeId,
             int? viewId,
             int? statusId,
             string sortBy,
             SortOrderEnum sortOrder,
             int pageNumber = 1,
             int pageSize = 10);


        Task<IEnumerable<UnitTypeDTO>> GetUnitTypes();



        Task<CommandResult> CreateProperty(PropertyCreateDTO model);
        Task<CommandResult> CreateTower(TowerCreateDTO model);
        Task<CommandResult> CreateFloor(FloorCreateDTO model);
        Task<CommandResult> CreateUnit(UnitCreateDTO model);

    }
}
