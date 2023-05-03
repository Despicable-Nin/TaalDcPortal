namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ParkingUnitAvailabilityPerFloorDTO
{
    public ParkingUnitAvailabilityPerFloorDTO(int unitTypeId, string unitType, int floorId, string floor, int available)
    {
        UnitTypeId = unitTypeId;
        UnitType = unitType;
        FloorId = floorId;
        Floor = floor;
        Available = available;
    }

    public int UnitTypeId { get; set; }
    public string UnitType { get; }

    public int FloorId { get; set; }
    public string Floor { get; }
    public int Available { get; }
}