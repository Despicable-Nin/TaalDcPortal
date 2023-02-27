using Taaldc.Sales.Api.Application.Common.Models;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public interface IOrderQueries
{
    Task<PaginationQueryResult<Unit_Order_DTO>> GetUnitAndOrdersByAvailability(
        int unitStatusId,
        int pageNumber,
        int pageSize,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        string broker = "");

    Task<IEnumerable<PaymentDTO>> GetPayments(int id);

    Task<Unit_Order_DTO> GetOrder(int id);
    Task<object> GetBuyerContractDetails(int buyerId);
}

// public enum UnitIs : int
// {
//     AVAILABLE = 1,
//     SOLD = 2,
//     RESERVED = 3,
//     BLOCKED = 4
// }