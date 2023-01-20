

using Microsoft.EntityFrameworkCore;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public partial class DashboardQueries
{
    public async Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerParkingUnitType()
    {

        var unitDb = _context.Units.AsNoTracking().Where(unit =>
            unit.UnitTypeId.IsAParkingUnit() && unit.UnitStatusId == (int)UnitStatus.AVAILABLE);
        
        var ret = from unit in unitDb
            select new
            {
                unit.UnitType,
                floorArea = unit.GetFloorArea(),
                min = unitDb.Min(i => i.OriginalPrice),
                max = unitDb.Max(i => i.OriginalPrice),
                available = _context.Units.AsNoTracking().Count(u => u.UnitTypeId == unit.UnitTypeId)
            };

        var result = await ret.Select(i =>
                new AvailabilityPerUnitTypeDTO(i.UnitType, i.floorArea, ToPriceRange(i.min, i.max), i.available))
            .ToArrayAsync();

        return result;
    }

    public async Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerCondoUnitType()
    {
       
        var unitDb = _context.Units.AsNoTracking().Where(unit =>
            unit.UnitTypeId.IsACondoUnit() && unit.UnitStatusId == (int)UnitStatus.AVAILABLE);

        var ret = from unit in unitDb
            select new
            {
                unit.UnitType,
                floorArea = unit.GetFloorArea(),
                min = unitDb.Min(i => i.OriginalPrice),
                max = unitDb.Max(i => i.OriginalPrice),
                available = _context.Units.AsNoTracking().Count(u => u.UnitTypeId == unit.UnitTypeId)
            };

        var result = await ret.Select(i =>
                new AvailabilityPerUnitTypeDTO(i.UnitType, i.floorArea, ToPriceRange(i.min, i.max), i.available))
            .ToArrayAsync();

        return result;
    }

    private string ToPriceRange(decimal min, decimal max)
    {
        var a = min.ToString("#,##0.00");
        var b = max.ToString("#,##0.00");

        return $"PHP {a} - PHP {b}";
    }
}