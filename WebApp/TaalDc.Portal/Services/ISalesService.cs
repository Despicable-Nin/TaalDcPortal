using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.DTO.Sales.Buyer;
using TaalDc.Portal.Models;

namespace TaalDc.Portal.Services;

public interface ISalesService
{
    Task<PaginationQueryResult<OrderUnitBuyer_ClientDto>> GetUnitAndOrdersAvailability(
        int unitStatusId,
        int pageNumber,
        int pageSize,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        string broker = "");

    Task<SellUnitCommandResult> SellUnit(SalesCreate_ClientDto model);

    Task<OrderUnitBuyer_ClientDto> GetSalesById(int id);

    Task<IEnumerable<Payment_ClientDto>> GetSalesPayments(int id);

    Task<CommandResult> AcceptPayment(int orderId, int paymentId);
    Task<CommandResult> VoidPayment(int orderId, int paymentId);

    Task<CommandResult> AddPayment(PaymentCreate_ClientDto model);


    Task<IEnumerable<UnitCountByStatus_ClientDto>> GetResidentialUnitsCountByStatus();

    Task<IEnumerable<AvailabilityByUnitType_ClientDto>> GetResidentialAvailabilityByType();

    Task<IEnumerable<AvailabilityByView_ClientDto>> GetResidentialAvailabilityByView();
    
    Task<IEnumerable<UnitCountByStatus_ClientDto>> GetParkingUnitsCountByStatus();

    Task<IEnumerable<ParkingUnitAvailabilityPerUnitType_ClientDto>> GetAvailabilityPerParkingUnitType();

    Task<IEnumerable<ParkingUnitAvailabilityPerFloor_ClientDto>> GetParkingUnitTypeAvailabilityPerFloor();


    #region Buyer
    Task<int> AddBuyer(BuyerCreateAPI_ClientDto model);

    #endregion

}