namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record AvailabilityPerUnitTypeDTO
{
    public AvailabilityPerUnitTypeDTO(string unitType, double floorArea, string unitPriceRange, int count)
    {
        UnitType = unitType;
        FloorArea = floorArea;
        UnitPriceRange = unitPriceRange;
        Count = count;
    }

    public string UnitType { get; private set; }
    public double FloorArea { get; private set; }
    public string UnitPriceRange { get; private set; }
    public int Count { get; private set; }
}