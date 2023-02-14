using Dapper;
using Microsoft.Data.SqlClient;

namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public class BuyerQueries : IBuyerQueries
{
    private readonly string SELECT_BUYER_COUNT = " SELECT COUNT(*) AS Total FROM [taaldb_sales].[sales].[buyer] ";
    private readonly string _connectionString;

    public BuyerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<BuyerDto> GetBuyerById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckIfBuyerExists(int id)
    {
        var query = $"{SELECT_BUYER_COUNT} WHERE Id={id}";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var result = await connection.QueryAsync<int>(query);

        return result.SingleOrDefault() > 0;

    }
}