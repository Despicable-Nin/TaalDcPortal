using SeedWork;
using TaalDc.Portal.DTO.Sales;

namespace TaalDc.Portal.Services;

public interface ISalesService
{
    Task<IEnumerable<Unit_Order_DTO>> GetUnitAndOrdersAvailability();
}

public class SalesService : ISalesService, IAggregateRoot
{
    public Task<IEnumerable<Unit_Order_DTO>> GetUnitAndOrdersAvailability()
    {
        throw new NotImplementedException();
    }
}