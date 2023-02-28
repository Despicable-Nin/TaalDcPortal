using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Api.Application.Queries.Buyers;

namespace Taaldc.Sales.Api.Application.Queries;
public interface IResultDto{}

public record CountResult : IResultDto
{
    public int Count { get; init; }    
}

public class QueryWrapper<T> where T : IResultDto
{
    private readonly string _connectionString;
    public static QueryWrapper<T> Create (string connectionString) => new (connectionString);
    
    private QueryWrapper(string connectionString) => _connectionString = connectionString;
    
    public async Task<IEnumerable<T>> Execute(string query) 
    {
        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<T>(query);

        return result;
    }
}