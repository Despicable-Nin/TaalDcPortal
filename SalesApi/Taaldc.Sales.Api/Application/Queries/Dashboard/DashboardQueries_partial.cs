
using Microsoft.EntityFrameworkCore;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public partial class DashboardQueries
{
    public async Task<IEnumerable<AvailabilityOfParkingUnitPerFloorDTO>> GetParkingUnitTypeAvailabilityPerFloor()
    {
        var unitDb = _context.Units.AsNoTracking()
            .Where(unit =>  new[] { 2, 3, 4, 5, 8 }.Contains(unit.UnitTypeId)&& unit.UnitStatusId == (int)UnitStatus.AVAILABLE);

        var ret = from unit in unitDb
            select new AvailabilityOfParkingUnitPerFloorDTO(
                unit.UnitType,
                unitDb.Count(c => c.UnitTypeId == unit.UnitTypeId),
                unitDb.Select(c => new ParkingUnitAvailabilityPerFloorDTO(c.Floor,
                    unitDb.Count(x => x.UnitTypeId == unit.UnitTypeId && x.FloorId == unit.FloorId)))
            );

        return await ret.ToArrayAsync();

    }

    public async Task<AvailabilityOfResidentialUnitsPerViewDTO> GetResidentaialUnitAvailabilityPerView()
    {
        var unitDb = _context.Units.AsNoTracking()
            .Where(unit =>  new[] { 2, 3, 4, 5, 8 }.Contains(unit.UnitTypeId)&& unit.UnitStatusId == (int)UnitStatus.AVAILABLE);

        var ret = from unit in unitDb
            select new ResidentialUnitCountPerViewDTO(unit.ScenicView,
                unitDb.Count(i => i.UnitTypeId == unit.UnitTypeId));
        
        return new AvailabilityOfResidentialUnitsPerViewDTO(unitDb.Count(), await ret.ToArrayAsync());

    }

    public async Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerParkingUnitType()
    {

        var unitDb = _context.Units.AsNoTracking()
            .Where(unit =>  new[] { 6,7 }.Contains(unit.UnitTypeId) && unit.UnitStatusId == (int)UnitStatus.AVAILABLE);

        return await GetAvailabilityPerUnitType(unitDb);
    }

    public async Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerResidentialUnitType()
    {
       
        var unitDb = _context.Units.AsNoTracking().Where(unit =>
            new[] { 2, 3, 4, 5, 8 }.Contains(unit.UnitTypeId)&& unit.UnitStatusId == (int)UnitStatus.AVAILABLE);

        return await GetAvailabilityPerUnitType(unitDb);
    }
    
    private async Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerUnitType(
        IQueryable<UnitReplica> unitDb)
    {
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