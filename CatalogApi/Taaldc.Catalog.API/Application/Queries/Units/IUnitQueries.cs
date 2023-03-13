using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries.Units;

namespace Taaldc.Catalog.API.Application.Queries;

public interface IUnitQueries
{
    Task<PaginatedAvailableUnitsQueryResult> GetAvailableUnitsAsync(
        string? filter,
        int? unitTypeId,
        int? viewId,
        int? floorId,
        string location,
        int min = 0,
        int max = 999999999,
        int pageSize = 20, int pageNumber = 1);

    Task<IEnumerable<UnitTypeAvailabilityQueryResult>> GetUnitTypeAvailabilityByTowerIdAsync(int towerId);

    Task<PaginationQueryResult<UnitQueryResult>> GetActiveUnitsAsync(
        string filter,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int? statusId,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10
    );

    Task<UnitQueryResult> GetUnitByIdAsync(int unitId);

    Task<IEnumerable<UnitColorScheme>> GetUnitColorSchemeByFloorIdAsync(int floorId);
}

public record UnitColorScheme
{
    public string Floor { get; init; }
    public string FilePath { get; init; }
    public int UnitId { get; init; }
    public string Unit { get; init; }
    public int UnitStatus { get; init; }
    public string Color { get; init; }
}