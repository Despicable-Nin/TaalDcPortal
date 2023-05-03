using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Floors;

public interface IFloorQueries
{
    Task<IEnumerable<AvailableFloorsQueryResult>> GetAvailableFloorsByUnitType(int? unitTypeId);

    Task<PaginationQueryResult<FloorQueryResult>> GetActiveFloors(
        string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10
    );

    Task<FloorQueryResult> GetFloorById(int id);

    Task<IEnumerable<ActiveFloorQueryResult>> GetActiveFloorsByTowerId(int towerId);

}