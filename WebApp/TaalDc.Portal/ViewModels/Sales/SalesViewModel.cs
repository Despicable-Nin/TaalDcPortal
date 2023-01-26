using TaalDc.Portal.DTO.Sales;

namespace TaalDc.Portal.ViewModels.Sales;

public class SalesViewModel
{
    public SalesViewModel(IEnumerable<Payment_ClientDto> payments, int orderId)
    {
        Payments = payments;
        OrderId = orderId;
    }


    public IEnumerable<Payment_ClientDto> Payments { get; }
    public int OrderId { get; }
}