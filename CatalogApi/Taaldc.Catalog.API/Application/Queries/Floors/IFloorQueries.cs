using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Taaldc.Catalog.API.Application.Queries.Units;
using Dapper;
using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Floors
{
	public interface IFloorQueries
	{
		Task<IEnumerable<AvailableFloorsQueryResult>> GetAvailableFloorsByUnitType(int? unitTypeId);
		Task<PaginationQueryResult<FloorQueryResult>> GetActiveFloors(
			 string filter,
			 string sortBy,
			 SortOrderEnum sortOrder,
			 int pageNumber = 1,
			 int pageSize = 10
		);

		Task<FloorQueryResult> GetFloorById(int id);
	}

	public class FloorQueries : IFloorQueries
	{
		private readonly string _connectionString;

		public FloorQueries(string connectionString)
		{
			_connectionString = string.IsNullOrWhiteSpace(connectionString)
			? throw new ArgumentNullException(nameof(connectionString))
			: connectionString;
		}

        public async Task<PaginationQueryResult<FloorQueryResult>> GetActiveFloors(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            var query = $"SELECT f.Id," +
				$"t.Id AS TowerId" +
				$",p.Id as PropertyId" +
				$",p.[Name] AS PropertyName" +
				$",t.[Name] AS TowerName" +
				$",f.[Name] AS FloorName" +
				$",f.[Description] AS FloorDescription" +
				$",f.[FloorPlanFilePath]," +
				$"COUNT(u.Id) AS Units " +
				$"FROM catalog.floors f " +
				$"JOIN catalog.tower t " +
				$"ON f.TowerId = t.Id " +
				$"JOIN catalog.property p " +
				$"ON t.PropertyId = p.Id " +
				$"LEFT JOIN catalog.unit u " +
				$"ON u.FloorId = f.Id  " +
				$"AND u.IsActive = '1' " +
				$"WHERE f.IsActive = '1'  AND " +
				$"p.IsActive = '1' AND " +
				$"(f.[Name] LIKE '%' + ISNULL('{filter}',f.[Name]) + '%' OR  " +
				$"f.[Description] LIKE '%' + ISNULL('{filter}',f.[Description]) + '%') " +
				$"GROUP BY f.Id, t.Id, p.Id, p.[Name], t.[Name], f.[Name], f.[Description], f.[FloorPlanFilePath]" +
				$"ORDER BY {PropertyHelper.GetFloorSorterName(sortBy, sortOrder)} " +
				$"OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(CancellationToken.None);

            var result = await connection.QueryAsync<FloorQueryResult>(query);

            var countQuery = $"WITH floor_cte AS (" +
				$"SELECT f.Id, t.Id AS TowerId, p.Id AS PropertyId " +
				$"FROM catalog.floors f " +
				$"JOIN catalog.tower t " +
				$"ON f.TowerId = t.Id " +
				$"JOIN catalog.property p " +
				$"ON t.PropertyId = p.Id " +
				$"WHERE f.IsActive = '1' " +
				$"AND p.IsActive = '1' " +
				$"AND (f.[Name] LIKE '%' + COALESCE('{filter}',f.[Name]) + '%' OR  " +
				$"f.[Description] LIKE '%' + COALESCE('{filter}',f.[Description]) + '%'))  " +
				$"SELECT TOP 1 COUNT(f.Id) FROM floor_cte f";

            var temp = await connection.QueryAsync<int>(countQuery);

            return new PaginationQueryResult<FloorQueryResult>(pageSize, pageNumber, temp.SingleOrDefault(), result);
        }

        public async Task<FloorQueryResult> GetFloorById(int id)
        {
	        var query = "SELECT DISTINCT f.Id " +
	                    $",t.Id AS [TowerId] " +
	                    $",p.Id as [PropertyId] " +
	                    $",p.[Name] AS [PropertyName] " +
	                    $",t.[Name] AS [TowerName] " +
	                    $",f.[Name] AS [FloorName] " +
	                    $",f.[Description] AS [FloorDescription] " +
	                    $",f.[FloorPlanFilePath] " +
	                    $",(SELECT COUNT(Id) FROM [taaldb_admin].[catalog].[unit] WHERE FloorId = f.Id) AS Units " +
	                    $"FROM catalog.floors f " +
	                    $"JOIN catalog.tower t ON f.TowerId = t.Id " +
	                    $"JOIN catalog.property p ON t.PropertyId = p.Id " +
	                    $"LEFT JOIN catalog.unit u ON u.FloorId = f.Id  " +
	                    $"WHERE f.Id = {id} ";
	                   

	        await using var connection = new SqlConnection(_connectionString);

	        await connection.OpenAsync();

	        var result = await connection.QueryAsync<FloorQueryResult>(query);

	        return result.FirstOrDefault();

        }

        public async Task<IEnumerable<AvailableFloorsQueryResult>> GetAvailableFloorsByUnitType(int? unitTypeId)
		{
			var availableFloorsQuery = $"SELECT " +
			$"DISTINCT(f.Id) AS FloorId, " +
			$"f.Name AS FloorName, " +
			$"f.FloorPlanFilePath AS FloorPlanFilePath " +
			$"FROM taaldb_admin.catalog.floors f " +
			$"LEFT JOIN catalog.unit u " +
			$"ON u.FloorId = f.Id " +
			$"AND u.UnitStatus = 1 " +
			$"AND u.UnitType = {(unitTypeId == null? "u.UnitType": unitTypeId)} " +
			$"WHERE u.Id IS NOT NULL";

			await using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync(CancellationToken.None);

			var result = await connection.QueryAsync<AvailableFloorsQueryResult>(availableFloorsQuery);

			return result;
		}
	}
}
