using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Queries.Units;

namespace Taaldc.Catalog.API.Application.Queries;

public interface IUnitQueries
{
    Task<AvailableUnitQueryResult> GetAvailableUnitsAsync(int? unitTypeId, int? viewId, int? floorId, int min = 0,
        int max = 999999999, int pageSize = 20, int pageNumber = 1);

    Task<IEnumerable<UnitTypeAvailability>> GetUnitTypeAvailabilityByTowerId(int towerId);
}

public class UnitQueries : IUnitQueries
{
    private readonly string _connectionString;

    public UnitQueries(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentNullException(nameof(connectionString))
            : connectionString;
    }

    public async Task<AvailableUnitQueryResult> GetAvailableUnitsAsync(int? unitTypeId, int? viewId, int? floorId,
        int min = 0,
        int max = 999999999, int pageSize = 20, int pageNumber = 1)
    {
        var query =
            " SELECT U.[Id], U.[Identifier], U.[Price], U.[FloorArea], F.Name [Floor], F.[Description] [FloorDesc], S.Name [View], US.Name [Status], UT.Name [Type], UT.ShortCode [TypeCode] ";
        query += " FROM [taaldb_admin].[catalog].[unit] U JOIN [taaldb_admin].[catalog].floors F ON U.FloorId = F.Id ";
        query += " JOIN [taaldb_admin].[catalog].scenicview S ON U.ScenicViewId = S.Id ";
        query += " JOIN [taaldb_admin].[catalog].unitstatus US ON U.UnitStatus = US.Id ";
        query += " JOIN taaldb_admin.[catalog].unittype UT ON U.UnitType = UT.Id ";

        List<string> clauses = new();

        if (unitTypeId.HasValue) clauses.Add($"UT.Id = {unitTypeId.Value}");


        if (viewId.HasValue) clauses.Add($"S.Id = {viewId.Value}");

        if (floorId.HasValue) clauses.Add($"F.Id = {floorId.Value}");

        clauses.Add($"(U.Price BETWEEN {min} AND {max})");
        clauses.Add("US.Id = 1");

        var where = string.Empty;
        if (clauses.Any()) where = " WHERE " + string.Join(" AND ", clauses.ToArray());

        if (!string.IsNullOrWhiteSpace(where)) query += where;

        query += $" ORDER BY U.Id OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY ";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<AvailableUnit>(query);

        var countQuery = " SELECT COUNT(*) [Total] ";
        countQuery +=
            " FROM [taaldb_admin].[catalog].[unit] U JOIN [taaldb_admin].[catalog].floors F ON U.FloorId = F.Id ";
        countQuery += " JOIN [taaldb_admin].[catalog].scenicview S ON U.ScenicViewId = S.Id ";
        countQuery += " JOIN [taaldb_admin].[catalog].unitstatus US ON U.UnitStatus = US.Id ";
        countQuery += " JOIN taaldb_admin.[catalog].unittype UT ON U.UnitType = UT.Id ";
        countQuery += where;

        var temp = await connection.QueryAsync<int>(countQuery);

        return new AvailableUnitQueryResult(pageSize, pageNumber, temp.Single(), result.ToArray());
    
    }

	public async Task<IEnumerable<UnitTypeAvailability>> GetUnitTypeAvailabilityByTowerId(int towerId)
	{
        var query = $"SELECT f.TowerId," +
            $"ut.Id AS UnitTypeId," +
            $"ut.ShortCode AS UnitTypeCode," +
            $"ut.[Name] AS UnitType," +
            $"ISNULL(MIN(CONVERT(DECIMAL(10,2),u.FloorArea)),0) AS MinFloorArea," +
            $"ISNULL(MAX(CONVERT(DECIMAL(10,2),u.FloorArea,2)),0) AS MaxFloorArea," +
            $"ISNULL(MIN(CONVERT(DECIMAL(10,2),u.Price)),0) AS MinPrice," +
            $"ISNULL(MAX(CONVERT(DECIMAL(10,2),u.Price,2)),0) AS MaxPrice," +
            $"COUNT(u.UnitType) AS Available " +
            $"FROM [taaldb_admin].catalog.unittype ut " +
            $"LEFT JOIN [taaldb_admin].catalog.unit u " +
            $"ON u.UnitType = ut.Id " +
            $"JOIN [taaldb_admin].catalog.floors f " +
            $"ON f.Id = u.FloorId " +
            $"WHERE f.TowerId = {towerId} " +
            $"GROUP BY u.UnitType, ut.[Name]," +
            $"ut.ShortCode,ut.Id,f.TowerId " +
            $"ORDER BY ut.Id";
		
        await using var connection = new SqlConnection(_connectionString);
		await connection.OpenAsync(CancellationToken.None);

		var result = await connection.QueryAsync<UnitTypeAvailability>(query);

        return result;
	}
}