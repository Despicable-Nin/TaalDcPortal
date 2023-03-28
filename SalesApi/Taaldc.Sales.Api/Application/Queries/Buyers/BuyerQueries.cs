using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Api.Application.Common.Models;

namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public class BuyerQueries : IBuyerQueries
{
    private readonly string SELECT_BUYER_QUERY =
        @"SELECT 
          B.Id [BuyerId]
          ,B.Salutation
          ,B.FirstName
          ,B.MiddleName
          ,B.LastName
          ,B.DoB
          ,C.Name [CivilStatus]
          ,C.Id [CivilStatusId]
          ,B.EmailAddress
          ,b.MobileNo
          ,B.PhoneNo
          ,B.Occupation
          ,B.Tin
          ,B.GovIssuedId
          ,B.GovIssuedIdValidUntil
          ,B.PartnerId
          ,B.IsCorporate
          ,CO.Address [Company_Address]
          ,CO.CorpSec [Company_CorpSec]
          ,CO.TIN [Company_TIN]
          ,CO.EmailAddress [Company_EmailAddress]
          ,CO.FaxNo [Company_FaxNo]
          ,CO.Industry [Company_Industry]
          ,CO.MobileNo [Company_MobileNo]
          ,CO.Name [Company_Name]
          ,CO.PhoneNo [Company_PhoneNo]
          ,CO.President [Company_President]
          ,CO.SECRegNo [Company_SECRegNo]
          ,HA.Street [HomeAddress_Street]
          ,HA.City [HomeAddress_City]
          ,HA.[State] [HomeAddress_State]
          ,HA.[Country] [HomeAddress_Country]
          ,HA.ZipCode [HomeAddress_ZipCode]
          ,BuA.Street [BusinessAddress_Street]
          ,BuA.City [BusinessAddress_City]
          ,BuA.[State] [BusinessAddress_State]
          ,BuA.[Country] [BusinessAddress_Country]
          ,BuA.ZipCode [BusinessAddress_ZipCode]
          ,BA.Street [BillingAddress_Street]
          ,BA.City [BillingAddress_City]
          ,BA.[State] [BillingAddress_State]
          ,BA.[Country] [BillingAddress_Country]
          ,BA.ZipCode [BillingAddress_ZipCode]
        FROM [taaldb_sales].[sales].[buyer] B
          LEFT JOIN taaldb_sales.sales.civilStatus C ON C.Id = B.CivilStatusId
          LEFT JOIN taaldb_sales.sales.company CO ON CO.BuyerId = B.Id
          LEFT JOIN taaldb_sales.sales.address HA ON HA.BuyerId = B.Id AND HA.[Type] = 1
          LEFT JOIN taaldb_sales.sales.address BuA ON BuA.BuyerId = B.Id AND BuA.[Type] = 2
          LEFT JOIN taaldb_sales.sales.address BA ON BA.BuyerId = B.Id AND BA.[Type] = 3";
    
    
    private readonly string SELECT_BUYER_COUNT = " SELECT COUNT(*) AS Total FROM [taaldb_sales].[sales].[buyer] ";
    private readonly string _connectionString;

    public BuyerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<BuyerDto> GetBuyerByIdAsync(int id)
    {
        var query = @$"{SELECT_BUYER_QUERY} WHERE B.Id = {id}";
        
        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<BuyerDto>(query);

        return result.FirstOrDefault();

    }

    public async Task<PaginationQueryResult<BuyerDto>> GetPaginatedAsync(
        int pageNumber, 
        int pageSize, 
        string name, 
        string email, 
        int? civilStatusId,
        string createdBy)
    {
        var query = @$"{SELECT_BUYER_QUERY}";

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

        if (!string.IsNullOrEmpty(createdBy))
        {
            where += $"AND (B.CreatedBy = '{createdBy}' OR B.ModifiedBy = '{createdBy}') ";
        }

        query += where;
        query += @$" ORDER BY B.[Id]  
        OFFSET {(pageNumber - 1) * pageSize} 
        ROWS FETCH NEXT {pageSize} ROWS ONLY";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<BuyerDto>(query);

        var countQuery = @"SELECT COUNT(*) [Count] 
                            FROM  [taaldb_sales].[sales].[buyer] ";
        
        var count = await connection.QueryAsync<int>(countQuery);

        return new PaginationQueryResult<BuyerDto>(pageSize, pageNumber, count.SingleOrDefault(), result);

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