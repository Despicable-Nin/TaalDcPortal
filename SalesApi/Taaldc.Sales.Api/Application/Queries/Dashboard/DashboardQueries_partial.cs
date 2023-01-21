
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
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

    public async Task<IEnumerable<ParkingUnitAvailabilityPerUnitTypeDTO>> GetAvailabilityPerParkingUnitType()
    {

        var query = "SELECT DISTINCT U.[UnitType] " + 
                    ",U.[UnitArea] + U.[BalconyArea] [FloorArea] " +
                    ",(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice ASC) [Min] " +
                    ",(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice DESC) [Max] " +
                    ",(SELECT COUNT(*) FROM [taaldb_sales].sales.unitreplica WHERE U.UnitTypeId = UnitTypeId GROUP BY UnitTypeId) [Available] " +
                    "FROM [taaldb_sales].[sales].[unitreplica] U " +
                    "LEFT JOIN [taaldb_sales].[sales].[order] O ON O.UnitId = U.UnitId " +
                    "LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id " +
                    "WHERE U.UnitTypeId IN (6,7) ";
        
        await using var connection = new SqlConnection(_connectionString);
      
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ParkingUnitAvailabilityPerUnitTypeDTO>(query);
        
        return result;
    }

    public async Task<IEnumerable<ResidentialUnitAvailabilityPerUnitTypeDTO>> GetAvailabilityPerResidentialUnitType()
    {
       
        var query = "SELECT DISTINCT U.[UnitType] " +
                    ",(SELECT TOP 1 UnitArea + BalconyArea FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice ASC) [MinArea] " +
                    ",(SELECT TOP 1 UnitArea + BalconyArea  FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice DESC) [MaxArea] " +
                    ",(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice ASC) [Min] " +
                    ",(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice DESC) [Max] " +
                    ",(SELECT COUNT(*) FROM [taaldb_sales].sales.unitreplica WHERE U.UnitTypeId = UnitTypeId GROUP BY UnitTypeId) [Available] " +
                    "FROM [taaldb_sales].[sales].[unitreplica] U " +
                    "LEFT JOIN [taaldb_sales].[sales].[order] O ON O.UnitId = U.UnitId " +
                    "LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id " +
                    "WHERE U.UnitTypeId IN (2,3,4,5,8) ";
        
        await using var connection = new SqlConnection(_connectionString);
      
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ResidentialUnitAvailabilityPerUnitTypeDTO>(query);
        
        return result;
    }
    

    
    private static string ToPriceRange(decimal min, decimal max)
    {
        var a = min.ToString("#,##0.00");
        var b = max.ToString("#,##0.00");

        return $"PHP {a} - PHP {b}";
    }
}