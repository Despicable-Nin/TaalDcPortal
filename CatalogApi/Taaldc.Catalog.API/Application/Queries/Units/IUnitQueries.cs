using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Queries.Units;

namespace Taaldc.Catalog.API.Application.Queries;

public interface IUnitQueries
{
    Task<AvailableUnitQueryResult> GetAvailableUnitsAsync(int? unitTypeId, int? viewId, int? floorId, int min = 0,
        int max = 999999999, int pageSize = 20, int pageNumber = 1);
}

public class UnitQueries : IUnitQueries
{
    private readonly string _connectionstring;

    public UnitQueries(string connectionString)
    {
        _connectionstring = string.IsNullOrWhiteSpace(connectionString) ? throw new ArgumentNullException(nameof(connectionString)) : connectionString;
    }
    
    public async Task<AvailableUnitQueryResult> GetAvailableUnitsAsync(int? unitTypeId, int? viewId, int? floorId, int min = 0,
        int max = 999999999, int pageSize = 20, int pageNumber = 1)
    {
        string query =
            " SELECT U.[Id], U.[Identifier], U.[Price], U.[FloorArea], F.Name [Floor], F.[Description] [FloorDesc], S.Name [View], US.Name [Status], UT.Name [Type], UT.ShortCode [TypeCode] ";
        //query += " (SELECT COUNT(*) FROM [taaldb].[catalog].unit WHERE UnitStatus = 1) [TotalCount], ";
        //query += $" (SELECT {pageNumber}) [PageNumber], ";
        //query += $" (SELECT {pageSize}) [PageSize] ";
        query += " FROM [taaldb].[catalog].[unit] U JOIN [taaldb].[catalog].floors F ON U.FloorId = F.Id ";
        query += " JOIN [taaldb].[catalog].scenicview S ON U.ScenicViewId = S.Id ";
        query += " JOIN [taaldb].[catalog].unitstatus US ON U.UnitStatus = US.Id ";
        query += " JOIN taaldb.[catalog].unittype UT ON U.UnitType = UT.Id ";

        var clauses = new List<string>();
      
        if (unitTypeId.HasValue)
        {
            clauses.Add($"UT.Id = {unitTypeId.Value}");
        }

        if (viewId.HasValue)
        {
            clauses.Add($"S.Id = {viewId.Value}");
        }
        
        if (floorId.HasValue)
        {
            clauses.Add($"F.Id = {floorId.Value}");
        }

      
            clauses.Add($"(U.Price BETWEEN {min} AND {max})");
        
        
        clauses.Add($"US.Id = 1");

        var where = string.Empty;
        if (clauses.Any())
        {
            where = " WHERE " + string.Join(" AND ", clauses.ToArray());
        }

        if (!string.IsNullOrWhiteSpace(where)) query += where;
        
        query += $" ORDER BY U.Id OFFSET {pageNumber - 1} ROWS FETCH NEXT {pageSize} ROWS ONLY ";

        using var connection = new SqlConnection(_connectionstring);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<AvailableUnit>(query);
        
        string countQuery = " SELECT COUNT(*) [Total] ";
        countQuery += " FROM [taaldb].[catalog].[unit] U JOIN [taaldb].[catalog].floors F ON U.FloorId = F.Id ";
        countQuery += " JOIN [taaldb].[catalog].scenicview S ON U.ScenicViewId = S.Id ";
        countQuery += " JOIN [taaldb].[catalog].unitstatus US ON U.UnitStatus = US.Id ";
        countQuery += " JOIN taaldb.[catalog].unittype UT ON U.UnitType = UT.Id ";
        countQuery += where;

        var temp = await connection.QueryAsync<int>(countQuery);

        return new AvailableUnitQueryResult(pageSize, pageNumber, temp.First(), result.ToArray());
    }   
}