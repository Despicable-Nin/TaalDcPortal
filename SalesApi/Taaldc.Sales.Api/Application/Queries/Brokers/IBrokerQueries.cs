namespace Taaldc.Sales.Api.Application.Queries.Brokers
{
    public interface IBrokerQueries
    {
        Task<BrokerDto> GetBrokerInfoByEmail(string email);
    }
}
