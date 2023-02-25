using TaalDc.Portal.DTO.Sales;

namespace TaalDc.Portal.ViewModels.Sales;

public class SalesViewModel
{
    public SalesViewModel(IEnumerable<GetSalesPaymentResponse> payments, int orderId)
    {
        Payments = payments;
        OrderId = orderId;
    }


    public IEnumerable<GetSalesPaymentResponse> Payments { get; }
    public int OrderId { get; }
}