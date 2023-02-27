using Dapper;
using Microsoft.Data.SqlClient;

namespace Taaldc.Sales.Api.Application.Queries.UnitReplicas;

public interface IUnitQueries
{
    Task<IEnumerable<UnitAvailability>> GetUnitAvailabilityAsync(int[] ids);
}

public class UnitQueries : IUnitQueries
{
    private readonly string _connectionString;

    private readonly string GET_UNIT_AVAILABILITY = 
        @"SELECT 
            [UnitId],
            (SELECT IIF(UnitStatusId = 1, 'true', 'false')) [IsAvailable],
            [UnitTypeId]
        FROM 
            [taaldb_sales].[sales].[unitreplica] ";

    public UnitQueries(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    
    public async Task<IEnumerable<UnitAvailability>> GetUnitAvailabilityAsync(int[] ids)
    {
        try
        {
            var query = $"{GET_UNIT_AVAILABILITY} WHERE [UnitId] IN ({string.Join(",", ids)})";
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<UnitAvailability>(query);

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

public record UnitAvailability
{
    public int UnitId { get; init; }
    public bool IsAvailable { get; init; }
    public int UnitTypeId { get; init; }
}