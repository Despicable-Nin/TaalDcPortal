using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.Queries.Reports
{
    public class ReportQueries : IReportQueries
    {
        private readonly string _connectionString;
        private const string RESERVED_WITH_NO_DP_QUERY = $@"
        WITH firstOrderItemCTE AS (
            SELECT DISTINCT(oi.OrderId) AS OrderId,
                oi.UnitId,
                u.Unit,
                u.Property,
                u.Tower,
                u.Floor,
                u.UnitType
                    FROM [sales].[orderitem] oi
                    LEFT JOIN [sales].[unitreplica] u
                    ON oi.UnitId = u.Id
                )
            SELECT 
                o.Id
                ,o.BuyerId
                ,b.FirstName + ' ' + b.LastName AS FullName
                ,b.EmailAddress 
                ,o.ReservationExpiresOn
                ,DATEDIFF(DAY, GETDATE(), o.ReservationExpiresOn) As NoOfDays
                    ,oi.Property
                ,oi.Tower
                ,oi.Floor
                ,oi.UnitId
                ,oi.UnitType
                ,oi.Unit
            FROM 
                [taaldb_sales].[dbo].[payment] payment 
                JOIN [taaldb_sales].[sales].[order] o ON payment.OrderId = o.Id
                JOIN [taaldb_sales].sales.buyer b ON o.BuyerId = b.Id
                JOIN firstOrderItemCTE oi ON o.Id = oi.OrderId
            WHERE
                payment.StatusId = 1 
                AND payment.OrderId NOT IN (SELECT OrderId FROM [taaldb_sales].[dbo].[payment] WHERE PaymentTypeId > 1 AND OrderId = payment.OrderId)
                AND DATEDIFF(DAY, GETDATE(), o.ReservationExpiresOn) = 0;

        WITH firstOrderItemCTE AS (
            SELECT DISTINCT(oi.OrderId) AS OrderId,
            oi.UnitId,
            u.Unit,
            u.Property,
            u.Tower,
            u.Floor,
            u.UnitType
                FROM [sales].[orderitem] oi
                LEFT JOIN [sales].[unitreplica] u
                ON oi.UnitId = u.Id
            )";

        private const string EXPIRING_RESERVATION_COUNT_QUERY = 
            @"SELECT 
                COUNT(o.Id) As NoOfExpired
            FROM [taaldb_sales].[dbo].[payment] payment 
                JOIN [taaldb_sales].[sales].[order] o ON payment.OrderId = o.Id
                JOIN [taaldb_sales].sales.buyer b ON o.BuyerId = b.Id
                JOIN firstOrderItemCTE oi ON o.Id = oi.OrderId
            WHERE 
                payment.StatusId = 1  
                AND payment.OrderId NOT IN (SELECT OrderId FROM [taaldb_sales].[dbo].[payment] WHERE PaymentTypeId > 1 AND OrderId = payment.OrderId)
                AND DATEDIFF(DAY, GETDATE(), o.ReservationExpiresOn) = 0";

        public ReportQueries(string connectionString)
        {
            _connectionString = connectionString ??
                          throw new SalesDomainException(nameof(OrderQueries),
                              new ArgumentNullException($"{connectionString} cannot be null."));
        }
        public async Task<IEnumerable<OrderReportDto>> GetOrdersByDate(DateTime from, DateTime to)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(CancellationToken.None);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("DateFrom", from);
            parameters.Add("DateTo", to);

            var orders = await connection.QueryAsync<OrderReportDto>("[sales].[spGetOrdersByDate]", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return orders;
        }

        public async Task<IEnumerable<ReservationWithNoDPDto>> GetReservationsWithNoDP()
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(CancellationToken.None);

            var reservations = await connection.QueryAsync<ReservationWithNoDPDto>(RESERVED_WITH_NO_DP_QUERY);

            return reservations;
        }

        public async Task<int> GetExpiringReservationCount()
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(CancellationToken.None);

            var count = await connection.QueryAsync<int>(EXPIRING_RESERVATION_COUNT_QUERY);

            return count.FirstOrDefault();
        }
    }
}
