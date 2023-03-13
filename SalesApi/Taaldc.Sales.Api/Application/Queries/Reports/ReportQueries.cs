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
    }
}
