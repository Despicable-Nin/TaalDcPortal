namespace Taaldc.Sales.Api.Application.Queries.Orders;

public interface IOrderQueries
{
    Task<IEnumerable<PreSellingDTO>> GetUnitsAvailability(
        int unitStatusId,
        int pageNumber,
        int pageSize,
        int? floorId,
        int? unitTypeId,
        int? viewId);
    
    
    Task<IEnumerable<PaymentDTO>> GetPayments(int id);
}

// public enum UnitIs : int
// {
//     AVAILABLE = 1,
//     SOLD = 2,
//     RESERVED = 3,
//     BLOCKED = 4
// }