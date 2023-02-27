using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Api.Application.Common.Models;

namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public class BuyerQueries : IBuyerQueries
{
   private readonly string _connectionString;

    public BuyerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<BuyerQueryDto> GetBuyerByIdAsync(int id)
    {
        var result = await QueryWrapper<BuyerQueryDto>
            .Create(_connectionString)
            .Execute(@$"{BuyerSQL.SELECT_BUYER_QUERY} WHERE B.Id = {id}");

        return result.FirstOrDefault();

    }

    public async Task<PaginationQueryResult<BuyerQueryDto>> GetPaginatedAsync(int pageNumber, int pageSize, string name, string email, int? civilStatusId)
    {
        var query = @$"{BuyerSQL.SELECT_BUYER_QUERY} ";

        var where = "WHERE B.IsActive = 1 ";

        if (!string.IsNullOrEmpty(name))
        {
            where += $"AND (B.[FirstName] LIKE '%{name}%' OR B.[LastName] LIKE '%{name}%') ";
        }

        if (!string.IsNullOrEmpty(email))
        {
            where += $"{(string.IsNullOrEmpty(name) ? "AND" : "OR")} B.[EmailAddress] LIKE '%{email}%' ";
        }

        if (civilStatusId.HasValue)
        {
            where += $"AND C.[Id] = {civilStatusId.HasValue} ";
        }

        query += where;
        query += @$" ORDER BY B.[LastName]  
        OFFSET {(pageNumber - 1) * pageSize} 
        ROWS FETCH NEXT {pageSize} ROWS ONLY";

        var result = await QueryWrapper<BuyerQueryDto>
            .Create(_connectionString)
            .Execute(query);

        var countQuery = @"SELECT COUNT(*) [Count] 
                            FROM  [taaldb_sales].[sales].[buyer] ";
        
        var countResult = await QueryWrapper<CountQuery>
            .Create(_connectionString)
            .Execute(countQuery);

        return new PaginationQueryResult<BuyerQueryDto>(pageSize, pageNumber,  countResult.SingleOrDefault().Count, result);

    }

    public async Task<bool> CheckIfBuyerExists(int id)
    {
        var query = $"{BuyerSQL.SELECT_BUYER_COUNT} WHERE Id={id}";
        var result = await QueryWrapper<CountQuery>
            .Create(_connectionString)
            .Execute(query);

        return result.SingleOrDefault()?.Count > 0;

    }
}