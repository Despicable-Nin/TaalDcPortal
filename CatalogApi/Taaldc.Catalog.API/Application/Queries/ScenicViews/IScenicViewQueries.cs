using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Queries.Floors;

namespace Taaldc.Catalog.API.Application.Queries.ScenicViews
{
	public interface IScenicViewQueries
	{
		Task<IEnumerable<AvailableViewQueryResult>> GetAvailableViewsByUnitType(int? unitTypeId);
	}

	public class ScenicViewQueries : IScenicViewQueries
	{
		private readonly string _connectionString;
		public ScenicViewQueries(string connectionString)
		{
			_connectionString = string.IsNullOrWhiteSpace(connectionString)
			? throw new ArgumentNullException(nameof(connectionString))
			: connectionString;
		}

		public async Task<IEnumerable<AvailableViewQueryResult>> GetAvailableViewsByUnitType(int? unitTypeId)
		{
			var availableViewsQuery = $"SELECT DISTINCT(sv.Id) AS ViewId," +
			$"sv.Name as ScenicView " +
			$"FROM catalog.scenicview sv " +
			$"LEFT JOIN catalog.unit u " +
			$"ON u.ScenicViewId = sv.Id " +
			$"AND u.UnitStatus = 1 AND " +
			$"u.UnitType = {(unitTypeId == null? "u.UnitType": unitTypeId)} WHERE " +
			$"u.Id IS NOT NULL";

			await using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync(CancellationToken.None);

			var result = await connection.QueryAsync<AvailableViewQueryResult>(availableViewsQuery);

			return result;
		}
	}
}
