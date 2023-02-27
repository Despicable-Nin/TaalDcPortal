using Taaldc.Sales.Api.Application.Common.Models;

namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public interface IBuyerQueries
{
    Task<BuyerQueryDto> GetBuyerByIdAsync(int id);
    Task<PaginationQueryResult<BuyerQueryDto>> GetPaginatedAsync(int pageNumber,int pageSize, string name,string email, int? civilStatusId);
    Task<bool> CheckIfBuyerExists(int id);
}
