using System.Collections;
using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Api.Application.Common.Models;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public class OrderQueries : IOrderQueries
{
  
    private readonly string _connectionString;


    public OrderQueries(string connectionString)
    {
        _connectionString = connectionString ??
                            throw new SalesDomainException(nameof(OrderQueries),
                                new ArgumentNullException($"{connectionString} cannot be null."));

    }

    public async Task<PaginationQueryResult<Unit_Order_DTO>> GetUnitAndOrdersByAvailability(
        int unitStatusId,
        int pageNumber,
        int pageSize,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        string broker = "")
    {
        var brokerString = string.IsNullOrEmpty(broker) ? "" : $" O.[Broker] = '{broker}' AND ";

        var query =  @$"{OrderSQL.SELECT_UNITS_WITH_BUYER} WHERE {brokerString} U.UnitStatusId = ISNULL({(unitStatusId > 0 ? $"'{unitStatusId}'" : "NULL")}, U.UnitStatusId) 
            AND U.FloorId = ISNULL({(floorId > 0 ? $"'{floorId}'" : "NULL")}, U.FloorId) 
            AND U.UnitTypeId = ISNULL({(unitTypeId > 0 ? $"'{unitTypeId}'" : "NULL")}, U.UnitTypeId) 
            AND U.ScenicViewId = ISNULL({(viewId > 0 ? $"'{viewId}'" : "NULL")},U.ScenicViewId) 
        ORDER BY U.[UnitId] 
        OFFSET {(pageNumber - 1) * pageSize} 
        ROWS FETCH NEXT {pageSize} ROWS ONLY";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<Unit_Order_DTO>(query);


        //get total count
        var unitCountQuery =
            @"SELECT 
                TOP 1 count(U.Id) 
            FROM 
                [taaldb_sales].[sales].[unitreplica] U 
                LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
                LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId ";

        var countQuery =
            @$"{unitCountQuery} 
            WHERE 
                {brokerString} U.UnitStatusId = ISNULL({(unitStatusId > 0 ? $"'{unitStatusId}'" : "NULL")}, U.UnitStatusId) 
                AND U.FloorId = ISNULL({(floorId > 0 ? $"'{floorId}'" : "NULL")}, U.FloorId) 
                AND U.UnitTypeId = ISNULL({(unitTypeId > 0 ? $"'{unitTypeId}'" : "NULL")}, U.UnitTypeId) 
                AND U.ScenicViewId = ISNULL({(viewId > 0 ? $"'{viewId}'" : "NULL")},U.ScenicViewId)";

        var count = await connection.QueryAsync<int>(countQuery);

        return new PaginationQueryResult<Unit_Order_DTO>(pageSize, pageNumber, count.SingleOrDefault(), result);
    }

    public async Task<IEnumerable<PaymentDTO>> GetPayments(int id)
    {
        var query = OrderSQL.SELECT_PAYMENTS(id);

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<PaymentDTO>(query);

        return result;
    }

    public Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Unit_Order_DTO> GetOrder(int id)
    {
        var query = $"{OrderSQL.SELECT_UNITS_WITH_BUYER} WHERE O.Id = '{id}'";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<Unit_Order_DTO>(query);

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<ContractDto>> GetBuyerContractDetails(int buyerId)
    {
        var query = $"{OrderSQL.SELECT_ORDER_DETAILS} WHERE BuyerId = '{buyerId}'";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<ContractDto>(query);

        return result;
    }
}