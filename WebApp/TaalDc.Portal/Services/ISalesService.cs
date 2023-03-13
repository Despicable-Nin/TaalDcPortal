using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.DTO.Sales.Buyer;
using TaalDc.Portal.DTO.Sales.Contracts;
using TaalDc.Portal.Models;

namespace TaalDc.Portal.Services;

public interface ISalesService
{
    Task<PaginationQueryResult<GetSalesByIdResponse>> GetUnitAndOrdersAvailability(
        int unitStatusId,
        int pageNumber,
        int pageSize,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        string? filter,
        string broker = "");

    Task<AddBuyerOrderResponse> AddBuyerOrder(AddBuyerOrderRequest model);

    Task<GetSalesByIdResponse> GetSalesById(int id);

    Task<IEnumerable<GetSalesPaymentResponse>> GetSalesPayments(int id);

    Task<Response> AcceptPayment(int orderId, int paymentId, string confirmationNumber);
    Task<Response> VoidPayment(int orderId, int paymentId);

    Task<Response> AddPayment(AddPaymentRequest model);


    Task<IEnumerable<GetResidentialUnitsCountByStatusResponse>> GetResidentialUnitsCountByStatus();

    Task<IEnumerable<GetResidentialAvailabilityByTypeResponse>> GetResidentialAvailabilityByType();

    Task<IEnumerable<GetResidentialAvailabilityByViewResponse>> GetResidentialAvailabilityByView();
    
    Task<IEnumerable<GetResidentialUnitsCountByStatusResponse>> GetParkingUnitsCountByStatus();

    Task<IEnumerable<GetAvailabilityPerParkingUnitTypeResponse>> GetAvailabilityPerParkingUnitType();

    Task<IEnumerable<GetParkingUnitTypeAvailabilityPerFloorResponse>> GetParkingUnitTypeAvailabilityPerFloor();


    #region Buyer
    Task<Response> AddBuyer(AddBuyerRequest model);
    Task<Response> UpdateBuyerInfo(UpdateBuyerInfoRquest model);
    Task<bool> UpdateBuyerContact(UpdateBuyerContactRequest model);
    Task<Response> UpdateBuyerMisc(UpdateBuyerMiscRequest model);
    Task<Response> PatchBuyerAddress(PatchBuyerAddressRequest model);
    Task<Response> UpdateBuyerCompany(UpdateBuyerCompanyRequest model);
    Task<Response> UpsertSpouse(UpsertBuyerSpouseRequest model);
    Task<PaginationQueryResult<GetBuyerResponse>> GetBuyers(int pageNumber, int pageSize, string name, string email);
    Task<GetBuyerResponse> GetBuyer(int buyerId);
    #endregion


    #region Contracts
    Task<Response> CreateContract(CreateContractRequest model);
    Task<IEnumerable<ContractOrderItem_ClientDto>> GetContractOrderItems(int id);
    #endregion

    Task<IEnumerable<Contract_ClientDto>> GetBuyerContracts(int id);
    Task<IEnumerable<OrderReportResponse>> GetOrdersByDate(DateTime from, DateTime to);
}