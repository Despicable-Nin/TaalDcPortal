using System.Collections;
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
        string filter,
        string broker = "");

    Task<IEnumerable<PaymentDTO>> GetPayments(int id);

    Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int id);

    Task<Unit_Order_DTO> GetOrder(int id);
    Task<IEnumerable<ContractDto>> GetBuyerContractDetails(int buyerId);
}

// public enum UnitIs : int
// {
//     AVAILABLE = 1,
//     SOLD = 2,
//     RESERVED = 3,
//     BLOCKED = 4
// }