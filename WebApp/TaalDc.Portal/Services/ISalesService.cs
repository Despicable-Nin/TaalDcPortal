using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Sales;

namespace TaalDc.Portal.Services;

public interface ISalesService
{
    Task<PaginationQueryResult<Unit_Order_DTO>> GetUnitAndOrdersAvailability(
		int unitStatusId,
		int pageNumber,
		int pageSize,
		int? floorId,
		int? unitTypeId,
		int? viewId,
		string broker = "");

    Task<SellUnitCommandResult> SellUnit(SalesCreateDTO model);

	Task<Unit_Order_DTO> GetSalesById(int id);

	Task<IEnumerable<PaymentDTO>> GetSalesPayments(int id);

	Task<CommandResult> AcceptPayment(int orderId, int paymentId);
}
