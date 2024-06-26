using Dapper;
using Microsoft.Data.SqlClient;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public partial class DashboardQueries
{
    public async Task<IEnumerable<ParkingUnitAvailabilityPerFloorDTO>> GetParkingUnitTypeAvailabilityPerFloor()
    {
        var query = @"SELECT DISTINCT 
                          U.UnitTypeId
                        , U.UnitType
                        , U.FloorId
                        , U.Floor 
                        , (SELECT COUNT(*) FROM [taaldb_sales].sales.unitreplica WHERE U.FloorId = FloorId AND UnitStatusId = 1 AND UnitTypeId = U.UnitTypeId) [Available] 
                    FROM [taaldb_sales].[sales].[unitreplica] U  
                        LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
                        LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId 
                        LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id 
                    WHERE U.UnitTypeId IN (6,7) AND U.UnitStatusId = 1 ";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ParkingUnitAvailabilityPerFloorDTO>(query);
        return result;
    }

    public async Task<IEnumerable<ResidentialUnitCountPerViewDTO>> GetResidentaialUnitAvailabilityPerView()
    {
        var query = @"SELECT DISTINCT 
                        U.[ScenicViewId] [ViewId]
                        ,U.[ScenicView] [View] 
                        ,(SELECT COUNT(*) FROM [taaldb_sales].sales.unitreplica WHERE U.ScenicViewId = ScenicViewId AND UnitStatusId = 1 GROUP BY ScenicViewId) [Available] 
                    FROM [taaldb_sales].[sales].[unitreplica] U 
                        LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
                        LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId 
                        LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id 
                    WHERE U.UnitTypeId IN (2,3,4,5,8) AND U.UnitStatusId = 1 ";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ResidentialUnitCountPerViewDTO>(query);
        
        return result;
    }

    public async Task<IEnumerable<ParkingUnitAvailabilityPerUnitTypeDTO>> GetAvailabilityPerParkingUnitType()
    {
        var query = @"SELECT DISTINCT 
                          U.[UnitTypeId]
                        , U.[UnitType] 
                        , U.[UnitArea] + U.[BalconyArea] [FloorArea] 
                        ,(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice ASC) [Min] 
                        ,(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice DESC) [Max] 
                        ,(SELECT COUNT(*) FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId AND UnitStatusId = 1 GROUP BY UnitTypeId) [Available] 
                    FROM [taaldb_sales].[sales].[unitreplica] U 
                        LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
                        LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId 
                        LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id 
                    WHERE U.UnitTypeId IN (6,7)  AND U.UnitStatusId = 1 ";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ParkingUnitAvailabilityPerUnitTypeDTO>(query);

        return result;
    }

    public async Task<IEnumerable<ResidentialUnitAvailabilityPerUnitTypeDTO>> GetAvailabilityPerResidentialUnitType()
    {
        var query = @"SELECT DISTINCT 
                        U.[UnitTypeId]
                        ,U.[UnitTypeShortCode]
                        ,(SELECT TOP 1 UnitArea + BalconyArea FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice ASC) [MinArea] 
                        ,(SELECT TOP 1 UnitArea + BalconyArea  FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice DESC) [MaxArea] 
                        ,(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice ASC) [Min] 
                        ,(SELECT TOP 1 OriginalPrice FROM [taaldb_sales].sales.unitreplica WHERE UnitTypeId = U.UnitTypeId ORDER BY OriginalPrice DESC) [Max] 
                        ,(SELECT COUNT(*) FROM [taaldb_sales].sales.unitreplica WHERE U.UnitTypeId = UnitTypeId AND UnitStatusId = 1 GROUP BY UnitTypeId) [Available] 
                    FROM [taaldb_sales].[sales].[unitreplica] U 
                        LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
                        LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId 
                        LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id 
                    WHERE U.UnitTypeId IN (2,3,4,5,8)  AND U.UnitStatusId = 1 ";

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