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
		int? viewId);

    Task<SellUnitCommandResult> SellUnit(SalesCreateDTO model);
}
