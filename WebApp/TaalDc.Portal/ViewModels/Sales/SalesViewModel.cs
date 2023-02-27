using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.DTO.Sales.Contracts;

namespace TaalDc.Portal.ViewModels.Sales;

public class SalesViewModel
{
    public SalesViewModel(IEnumerable<GetSalesPaymentResponse> payments, IEnumerable<ContractOrderItem_ClientDto> units, int orderId)
    {
        Payments = payments;
        Units = units;
        OrderId = orderId;
    }


    public IEnumerable<GetSalesPaymentResponse> Payments { get; }
    public IEnumerable<ContractOrderItem_ClientDto> Units { get; }
    public int OrderId { get; }
}