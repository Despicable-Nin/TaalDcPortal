namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public interface IBuyerQueries
{
    Task<BuyerDto> GetBuyerById(int id);
    Task<bool> CheckIfBuyerExists(int id);
}