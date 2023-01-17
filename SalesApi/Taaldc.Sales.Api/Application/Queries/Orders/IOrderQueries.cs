namespace Taaldc.Sales.Api.Application.Queries.Orders;

public interface IOrderQueries
{
    Task<IEnumerable<PreSellingDTO>> GetPresellingReport();
}

