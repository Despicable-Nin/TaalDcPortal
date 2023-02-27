namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record AvailabilityOfParkingUnitPerFloorDTO
{
    public AvailabilityOfParkingUnitPerFloorDTO(string unitType, int totalCount,
        IEnumerable<ParkingUnitAvailabilityPerFloorDTO> parkingUnitsDto)
    {
        UnitType = unitType;
        TotalCount = totalCount;
        ParkingUnitsDTO = parkingUnitsDto;
    }

    public string UnitType { get; }
    public int TotalCount { get; }
    public IEnumerable<ParkingUnitAvailabilityPerFloorDTO> ParkingUnitsDTO { get; }
}