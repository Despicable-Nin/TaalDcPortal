using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Taaldc.Catalog.API.Application.Queries.Units;
using Dapper;

namespace Taaldc.Catalog.API.Application.Queries.Floors
{
	public interface IFloorQueries
	{
		Task<IEnumerable<AvailableFloor>> GetAvailableFloorsByUnitType(int? unitTypeId);
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
		public async Task<IEnumerable<AvailableFloor>> GetAvailableFloorsByUnitType(int? unitTypeId)
		{
			var availableFloorsQuery = $"SELECT DISTINCT(f.Id) AS FloorId, " +
			$"f.Name AS FloorName " +
			$"FROM taaldb_admin.catalog.floors f " +
			$"LEFT JOIN catalog.unit u " +
			$"ON u.FloorId = f.Id " +
			$"AND u.UnitStatus = 1 " +
			$"AND u.UnitType = {(unitTypeId == null? "u.UnitType": unitTypeId)} " +
			$"WHERE u.Id IS NOT NULL";

			await using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync(CancellationToken.None);

			var result = await connection.QueryAsync<AvailableFloor>(availableFloorsQuery);

			return result;
		}
	}
}
