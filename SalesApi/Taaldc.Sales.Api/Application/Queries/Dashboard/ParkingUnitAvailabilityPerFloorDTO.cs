namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ParkingUnitAvailabilityPerFloorDTO
{
    public ParkingUnitAvailabilityPerFloorDTO(string floor, int available)
    {
        Floor = floor;
        Available = available;
    }

    public string Floor { get; private set; }
    public int Available { get; private set; }
}