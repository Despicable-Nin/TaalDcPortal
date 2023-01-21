using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Models;
using TaalDc.Portal.ViewModels.Sales;

namespace TaalDc.Portal.Services;

public interface ISalesService
{
    Task<IEnumerable<Unit_Order_DTO>> GetUnitAndOrdersAvailability();
    Task<SellUnitCommandResult> SellUnit(SalesCreateDTO model);
}
