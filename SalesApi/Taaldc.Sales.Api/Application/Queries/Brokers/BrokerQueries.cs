using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Api.Application.Queries.Reports;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Api.Application.Queries.Brokers
{
    public class BrokerQueries : IBrokerQueries
    {
        private readonly string _connectionString;

        public BrokerQueries(string connectionString)
        {
            _connectionString = connectionString ??
                          throw new SalesDomainException(nameof(OrderQueries),
                              new ArgumentNullException($"{connectionString} cannot be null."));
        }
        public async Task<BrokerDto> GetBrokerInfoByEmail(string email)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(CancellationToken.None);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmailAddress", email);

            var brokerInfo = await connection.QueryAsync<BrokerDto>("[sales].[spGetBrokerInfo]", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return brokerInfo.FirstOrDefault();
        }
    }
}
