namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public static class IntegerExtension
{
    public static bool IsACondoUnit(this int unitType)
    {
        return new[] { 2, 3, 4, 5, 8 }.Contains(unitType);
    }

    public static bool IsAParkingUnit(this int unitType)
    {
        return new[] { 6, 7 }.Contains(unitType);
    }
}