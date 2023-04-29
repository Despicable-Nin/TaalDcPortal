namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ParkingUnitAvailabilityPerUnitTypeDTO
{
    public ParkingUnitAvailabilityPerUnitTypeDTO(int unitTypeId, string unitType, double floorArea, decimal min, decimal max,
        int available)
    {
        UnitTypeId = unitTypeId;
        UnitType = unitType;
        FloorArea = floorArea;
        Min = min;
        Max = max;
        Available = available;
    }

    public int UnitTypeId { get; }
    public string UnitType { get; }
    public double FloorArea { get; }
    public decimal Min { get; }
    public decimal Max { get; }
    public string UnitPriceRange => ToPriceRange(Min, Max);
    public int Available { get; }

    private static string ToPriceRange(decimal min, decimal max)
    {
        var a = min.ToString("#,##0.00");
        var b = max.ToString("#,##0.00");

        return $"PHP {a} - PHP {b}";
    }
}

public record ResidentialUnitAvailabilityPerUnitTypeDTO
{
    public ResidentialUnitAvailabilityPerUnitTypeDTO(int unitTypeId, string unitTypeShortCode, double minArea, double maxArea,
        decimal min, decimal max, int available)
    {
        UnitTypeId = unitTypeId;
        UnitTypeShortCode = unitTypeShortCode;
        MinArea = minArea;
        MaxArea = maxArea;
        Min = min;
        Max = max;
        Available = available;
    }

    public int UnitTypeId { get; }
    public string UnitTypeShortCode { get; }
    public double MinArea { get; }
    public double MaxArea { get; }
    public string FloorArea => ToFloorArea(MinArea, MaxArea);
    public decimal Min { get; }
    public decimal Max { get; }
    public string UnitPriceRange => ToPriceRange(Min, Max);
    public int Available { get; }

    private static string ToFloorArea(double min, double max)
    {
        var a = min.ToString("#,##0.00");
        var b = max.ToString("#,##0.00");

        return $"{a} - {b}";
    }

    private static string ToPriceRange(decimal min, decimal max)
    {
        var a = min.ToString("#,##0.00");
        var b = max.ToString("#,##0.00");

        return $"PHP {a} - PHP {b}";
    }
}