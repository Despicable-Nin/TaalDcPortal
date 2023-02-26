using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Api.Application.Common.Models;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public class OrderQueries : IOrderQueries
{
    private const string PaymentCTE =
        "WITH PaymentCTE AS ( SELECT P.*, ROW_NUMBER() OVER (PARTITION BY P.OrderId ORDER BY P.id) as RowNum FROM [taaldb_sales].[dbo].[payment] P) ";

    private const string UnitQuery = @$"{PaymentCTE} SELECT  O.Id
        ,O.[Code] 
        ,O.[Broker] 
        ,O.[Remarks] 
        ,O.[StatusId]
        ,OS.[Name] AS Status
        ,O.[BuyerId] 
        ,B.[Salutation]
        ,B.[FirstName] 
        ,B.[LastName]
        ,B.[EmailAddress] 
        ,B.[PhoneNo] 
        ,B.[MobileNo] 
        ,A.[Street]
        ,A.[Country] 
        ,A.[State] 
        ,A.[City] 
        ,A.[ZipCode] 
        ,O.[Id] [OrderId] 
        ,B.[CreatedBy] 
        ,O.[BuyerId] 
        ,U.[PropertyId] 
        ,U.[TowerId] 
        ,U.[FloorId] 
        ,U.[UnitId] 
        ,U.[ScenicViewId]
        ,U.[UnitTypeId] 
        ,U.[Property] 
        ,U.[Tower] 
        ,U.[Floor] 
        ,U.[Unit] 
        ,U.[ScenicView] 
        ,U.[UnitType] 
        ,U.[UnitArea] 
        ,U.[BalconyArea] 
        ,U.[UnitStatus]
        ,U.[UnitStatusId]
        ,U.[OriginalPrice] 
        ,U.[SellingPrice]
        ,P.[ActualPaymentDate] AS TransactionDate 
    FROM [taaldb_sales].[sales].[unitreplica] U 
        LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
        LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId
        LEFT JOIN [taaldb_sales].[sales].[orderstatus] OS ON O.[StatusId] = OS.Id  
        LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id 
        LEFT JOIN [taaldb_sales].[sales].[address] A ON A.BuyerId = B.Id AND A.[Type] = 1
        LEFT JOIN  PaymentCTE P ON P.OrderId = O.Id AND P.RowNum = 1";

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

        var query =  @$"{UnitQuery} WHERE {brokerString} U.UnitStatusId = ISNULL({(unitStatusId > 0 ? $"'{unitStatusId}'" : "NULL")}, U.UnitStatusId) 
            AND U.FloorId = ISNULL({(floorId > 0 ? $"'{floorId}'" : "NULL")}, U.FloorId) 
            AND U.UnitTypeId = ISNULL({(unitTypeId > 0 ? $"'{unitTypeId}'" : "NULL")}, U.UnitTypeId) 
            AND U.ScenicViewId = ISNULL({(viewId > 0 ? $"'{viewId}'" : "NULL")},U.ScenicViewId) 
        ORDER BY O.[Id] 
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
        var query = "SELECT P.Id, " +
                    "P.ActualPaymentDate, " +
                    "P.ConfirmationNumber, " +
                    "P.PaymentTypeId, " +
                    "PT.[Name] AS PaymentType, " +
                    "P.TransactionTypeId, " +
                    "T.[Name] AS TransactionType, " +
                    "P.VerifiedBy, " +
                    "P.StatusId AS PaymentStatusId, " +
                    "PS.[Name] AS PaymentStatus, " +
                    "P.PaymentMethod, " +
                    "P.AmountPaid, " +
                    "ISNULL(P.Remarks, ''), " +
                    "P.CorrelationId, " +
                    "P.OrderId FROM dbo.payment P " +
                    "JOIN dbo.paymenttype PT ON " +
                    "P.PaymentTypeId = PT.Id JOIN " +
                    "dbo.transactiontype T ON " +
                    "P.TransactionTypeId = T.Id JOIN " +
                    "dbo.paymentstatus PS ON " +
                    "P.StatusId = PS.Id " +
                    $"WHERE OrderId = '{id}'";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<PaymentDTO>(query);

        return result;
    }

    public async Task<Unit_Order_DTO> GetOrder(int id)
    {
        var query = $"{UnitQuery} WHERE O.Id = '{id}'";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<Unit_Order_DTO>(query);

        foreach(var item in result)
        {
            item.Code = item.OrderId.ToString("00000");
        }

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int id)
    {
        var orderItemQuery = $"SELECT " +
            $"oi.Id," +
            $"OrderId," +
            $"oi.UnitId," +
            $"u.Unit AS Identifier," +
            $"u.UnitType," +
            $"u.Property," +
            $"u.Tower," +
            $"u.ScenicView," +
            $"u.[Floor]," +
            $"u.UnitArea," +
            $"u.BalconyArea," +
            $"u.OriginalPrice," +
            $"oi.Price," +
            $"u.UnitStatusId AS StatusId," +
            $"u.UnitStatus AS Status " +
            $"FROM [taaldb_sales].[sales].[orderitem] oi  " +
            $"JOIN [taaldb_sales].[sales].[order] o  " +
            $"ON oi.OrderId = o.Id  " +
            $"JOIN [taaldb_sales].[sales].[unitreplica] u  " +
            $"ON oi.UnitId = u.Id  WHERE o.Id = {id}";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<OrderItemDTO>(orderItemQuery);

        return result.ToList();
    }
}