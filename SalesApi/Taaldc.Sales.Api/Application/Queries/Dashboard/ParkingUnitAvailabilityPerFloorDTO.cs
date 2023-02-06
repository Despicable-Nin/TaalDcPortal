namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ParkingUnitAvailabilityPerFloorDTO
{
    public ParkingUnitAvailabilityPerFloorDTO(string unitType, string floor, int available)
    {
        UnitType = unitType;
        Floor = floor;
        Available = available;
    }

    public string UnitType { get; }
    public string Floor { get; }
    public int Available { get; }
}