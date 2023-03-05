using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Towers;

public interface ITowerQueries
{
    Task<PaginationQueryResult<TowerQueryResult>> GetActiveTowers(
        string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10
    );

    Task<TowerQueryResult> GetTowerById(int id);
}

public class TowerQueries : ITowerQueries
{
    private readonly string _connectionString;

    public TowerQueries(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentNullException(nameof(connectionString))
            : connectionString;
    }

    public async Task<PaginationQueryResult<TowerQueryResult>> GetActiveTowers(string filter, string sortBy,
        SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        var unitCTE = $"WITH unitcount_CTE AS(SELECT DISTINCT(f.TowerId) AS TowerId,Count(*) AS UnitCount FROM catalog.unit u JOIN catalog.floors f ON u.FloorId = f.Id WHERE u.IsActive = '1'AND u.ScenicViewId > 1 GROUP BY f.TowerId)";

        var query = $"{unitCTE} SELECT DISTINCT(t.Id)" +
                    ",p.[Id] AS PropertyId" +
                    ",p.[Name] AS PropertyName" +
                    ",t.[Name] AS TowerName" +
                    ",t.[Address]" +
                    ",unitCount.UnitCount AS Units  " +
                    "FROM catalog.tower t " +
                    "JOIN catalog.property p " +
                    "ON t.PropertyId = p.Id " +
                    "LEFT JOIN unitcount_CTE unitCount " +
                    "ON unitCount.TowerId = t.Id " +
                    "WHERE t.IsActive = '1'  " +
                    $"AND (t.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%'  OR " +
                    $"p.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%' ) " +
                    "GROUP BY t.Id, p.Id, p.[Name], t.[Name], t.[Address], unitCount.UnitCount " +
                    $"ORDER BY {PropertyHelper.GetTowerSorterName(sortBy, sortOrder)} " +
                    $"OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<TowerQueryResult>(query);

        var countQuery = "SELECT COUNT(*) " +
                         "FROM catalog.tower t " +
                         "JOIN catalog.property p " +
                         "ON t.PropertyId = p.Id " +
                         "WHERE t.IsActive = '1'  " +
                         $"AND (t.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%'  OR " +
                         $"p.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%' )";

        var temp = await connection.QueryAsync<int>(countQuery);

        return new PaginationQueryResult<TowerQueryResult>(pageSize, pageNumber, temp.SingleOrDefault(), result);
    }

    public async Task<TowerQueryResult> GetTowerById(int id)
    {
        var query = "SELECT t.Id" +
                    ",p.[Id] AS PropertyId" +
                    ",p.[Name] AS PropertyName" +
                    ",t.[Name] AS TowerName" +
                    ",t.[Address]" +
                    ",COUNT(u.[Id]) AS Units " +
                    "FROM catalog.tower t " +
                    "JOIN catalog.property p " +
                    "ON t.PropertyId = p.Id " +
                    "LEFT JOIN catalog.floors f " +
                    "ON f.TowerId = t.Id " +
                    "LEFT JOIN catalog.unit u " +
                    "ON u.FloorId = f.Id AND u.IsActive = '1' " +
                    "AND u.ScenicViewId > 1 " +
                    $"WHERE t.Id = '{id}'" +
                    "GROUP BY t.Id, p.Id, p.[Name], t.[Name], t.[Address]";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        try
        {
            var result = await connection.QueryFirstAsync<TowerQueryResult>(query);
            return result;
        }
        catch (Exception err)
        {
            Debug.Print(
                string.Join(", ", err.Message, err.StackTrace, err.InnerException?.Message));
            return null;
        }
    }
}