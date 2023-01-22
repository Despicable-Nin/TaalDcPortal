namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record AvailabilityOfParkingUnitPerFloorDTO
{
    public AvailabilityOfParkingUnitPerFloorDTO(string unitType, int totalCount, IEnumerable<ParkingUnitAvailabilityPerFloorDTO> parkingUnitsDto)
    {
        UnitType = unitType;
        TotalCount = totalCount;
        ParkingUnitsDTO = parkingUnitsDto;
    }

    public string UnitType { get; private set; }
    public int TotalCount { get; private set; }
    public IEnumerable<ParkingUnitAvailabilityPerFloorDTO> ParkingUnitsDTO { get; private set; }
}