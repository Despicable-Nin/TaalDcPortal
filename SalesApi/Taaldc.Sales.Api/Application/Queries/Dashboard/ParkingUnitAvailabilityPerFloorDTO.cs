namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ParkingUnitAvailabilityPerFloorDTO
{
    public ParkingUnitAvailabilityPerFloorDTO(string unitType, string floor, int available)
    {
        UnitType = unitType;
        Floor = floor;
        Available = available;
    }

    public string UnitType { get; private set; }
    public string Floor { get; private set; }
    public int Available { get; private set; }
}