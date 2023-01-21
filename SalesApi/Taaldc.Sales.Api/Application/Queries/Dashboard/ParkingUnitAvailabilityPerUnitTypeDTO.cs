using System.Text.Json.Serialization;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ParkingUnitAvailabilityPerUnitTypeDTO
{
    public ParkingUnitAvailabilityPerUnitTypeDTO(string unitType, double floorArea, decimal min, decimal max, int available)
    {
        UnitType = unitType;
        FloorArea = floorArea;
        Min = min;
        Max = max;
        Available = available;
    }


    public string UnitType { get; private set; }
    public double FloorArea { get; private set; }
    public decimal Min { get; private set; }
    public decimal Max { get; private set; }
    public string UnitPriceRange => ToPriceRange(Min, Max);
    public int Available { get; private set; }
    
    private static string ToPriceRange(decimal min, decimal max)
    {
        var a = min.ToString("#,##0.00");
        var b = max.ToString("#,##0.00");

        return $"PHP {a} - PHP {b}";
    }
}

public record ResidentialUnitAvailabilityPerUnitTypeDTO
{
    public ResidentialUnitAvailabilityPerUnitTypeDTO(string unitType, double minArea, double maxArea, decimal min, decimal max, int available)
    {
        UnitType = unitType;
        MinArea = minArea;
        MaxArea = maxArea;
        Min = min;
        Max = max;
        Available = available;
    }

    public string UnitType { get; private set; }
    public double MinArea { get; private set; }
    public double MaxArea { get; private set; }
    public string FloorArea => ToFloorArea(MinArea, MaxArea);
    public decimal Min { get; private set; }
    public decimal Max { get; private set; }
    public string UnitPriceRange => ToPriceRange(Min, Max);
    public int Available { get; private set; }
    
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