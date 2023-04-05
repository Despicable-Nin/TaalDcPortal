using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Floors;

public class FloorQueries : IFloorQueries
{
    private readonly string _connectionString;

    public FloorQueries(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentNullException(nameof(connectionString))
            : connectionString;
    }

    public async Task<PaginationQueryResult<FloorQueryResult>> GetActiveFloors(string filter, string sortBy,
        SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        var unitCountCTE = $"WITH unitcount_CTE AS(SELECT " +
                           $"DISTINCT(f.Id) AS FloorId, " +
                           $"Count(*) AS UnitCount " +
                           $"FROM catalog.unit u " +
                           $"JOIN catalog.floors f  " +
                           $"ON u.FloorId = f.Id " +
                           $"WHERE u.IsActive = '1' " +
                           $"GROUP BY f.Id)";

        var query = $"{unitCountCTE} SELECT f.Id," +
                    "t.Id AS TowerId" +
                    ",p.Id as PropertyId" +
                    ",p.[Name] AS PropertyName" +
                    ",t.[Name] AS TowerName" +
                    ",f.[Name] AS FloorName" +
                    ",f.[Description] AS FloorDescription" +
                    ",f.[FloorPlanFilePath]," +
                    "ISNULL(unitCount.UnitCount, 0) AS Units " +
                    "FROM catalog.floors f " +
                    "JOIN catalog.tower t " +
                    "ON f.TowerId = t.Id " +
                    "JOIN catalog.property p " +
                    "ON t.PropertyId = p.Id " +
                    "LEFT JOIN unitcount_CTE unitCount ON unitCount.FloorId = f.Id " +
                    "WHERE f.IsActive = '1'  AND " +
                    "p.IsActive = '1' AND " +
                    $"(f.[Name] LIKE '%' + ISNULL('{filter}',f.[Name]) + '%' OR  " +
                    $"f.[Description] LIKE '%' + ISNULL('{filter}',f.[Description]) + '%') " +
                    "GROUP BY f.Id, t.Id, p.Id, p.[Name], t.[Name], f.[Name], f.[Description], f.[FloorPlanFilePath], unitCount.UnitCount " +
                    $"ORDER BY {PropertyHelper.GetFloorSorterName(sortBy, sortOrder)} " +
                    $"OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<FloorQueryResult>(query);

        var countQuery = "WITH floor_cte AS (" +
                         "SELECT f.Id, t.Id AS TowerId, p.Id AS PropertyId " +
                         "FROM catalog.floors f " +
                         "JOIN catalog.tower t " +
                         "ON f.TowerId = t.Id " +
                         "JOIN catalog.property p " +
                         "ON t.PropertyId = p.Id " +
                         "WHERE f.IsActive = '1' " +
                         "AND p.IsActive = '1' " +
                         $"AND (f.[Name] LIKE '%' + COALESCE('{filter}',f.[Name]) + '%' OR  " +
                         $"f.[Description] LIKE '%' + COALESCE('{filter}',f.[Description]) + '%'))  " +
                         "SELECT TOP 1 COUNT(f.Id) FROM floor_cte f";

        var temp = await connection.QueryAsync<int>(countQuery);

        return new PaginationQueryResult<FloorQueryResult>(pageSize, pageNumber, temp.SingleOrDefault(), result);
    }

    public async Task<FloorQueryResult> GetFloorById(int id)
    {
        var query = "SELECT DISTINCT f.Id " +
                    ",t.Id AS [TowerId] " +
                    ",p.Id as [PropertyId] " +
                    ",p.[Name] AS [PropertyName] " +
                    ",t.[Name] AS [TowerName] " +
                    ",f.[Name] AS [FloorName] " +
                    ",f.[Description] AS [FloorDescription] " +
                    ",f.[FloorPlanFilePath] " +
                    ",(SELECT COUNT(Id) FROM [taaldb_admin].[catalog].[unit] WHERE FloorId = f.Id) AS Units " +
                    "FROM catalog.floors f " +
                    "JOIN catalog.tower t ON f.TowerId = t.Id " +
                    "JOIN catalog.property p ON t.PropertyId = p.Id " +
                    "LEFT JOIN catalog.unit u ON u.FloorId = f.Id  " +
                    $"WHERE f.Id = {id} ";


        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();

        var result = await connection.QueryAsync<FloorQueryResult>(query);

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<AvailableFloorsQueryResult>> GetAvailableFloorsByUnitType(int? unitTypeId)
    {
        var availableFloorsQuery = "SELECT " +
                                   "DISTINCT(f.Id) AS FloorId, " +
                                   "f.Name AS FloorName, " +
                                   "f.FloorPlanFilePath AS FloorPlanFilePath " +
                                   "FROM taaldb_admin.catalog.floors f " +
                                   "LEFT JOIN catalog.unit u " +
                                   "ON u.FloorId = f.Id " +
                                   "AND u.UnitStatus = 1 " +
                                   $"AND u.UnitType = {(unitTypeId == null ? "u.UnitType" : unitTypeId)} " +
                                   "WHERE u.Id IS NOT NULL";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<AvailableFloorsQueryResult>(availableFloorsQuery);

        return result;
    }

    public async Task<IEnumerable<ActiveFloorQueryResult>> GetActiveFloorsByTowerId(int towerId)
    {
        towerId = towerId == 0? 1: towerId;

        var activeFloorsQuery = @$"SELECT 
	                                f.[Id]
	                                ,f.[TowerId]
	                                ,f.[Name] AS FloorName
	                                ,f.[Description] AS FloorDescription
	                                ,f.[FloorPlanFilePath]
                                FROM [taaldb_admin].[catalog].[floors] f
                                WHERE f.IsActive = 1 
                                AND f.FloorPlanFilePath <> '' 
                                AND f.FloorPlanFilePath IS NOT NULL
                                AND f.[TowerId] = {towerId}";


        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ActiveFloorQueryResult>(activeFloorsQuery);

        return result;
    }
}