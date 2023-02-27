namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public interface IDashboardQueries
{
    Task<IEnumerable<UnitAvailabilitySummaryByStatusDTO>> GetCountByUnitStatus(int[] unitTypes);

    Task<int> GetAvailableUnitCount();
    Task<int> GetReservedUnitCount();
    Task<int> GetSoldUnitCount();
    Task<int> GetBlockedUnitCount();

    Task<int> GetAvailableParkingCount();
    Task<int> GetReservedParkingCount();
    Task<int> GetSoldParkingCount();
    Task<int> GetBlockedParkingCount();

    Task<IEnumerable<ParkingUnitAvailabilityPerFloorDTO>> GetParkingUnitTypeAvailabilityPerFloor();
    Task<IEnumerable<ResidentialUnitCountPerViewDTO>> GetResidentaialUnitAvailabilityPerView();
    Task<IEnumerable<ParkingUnitAvailabilityPerUnitTypeDTO>> GetAvailabilityPerParkingUnitType();
    Task<IEnumerable<ResidentialUnitAvailabilityPerUnitTypeDTO>> GetAvailabilityPerResidentialUnitType();
}