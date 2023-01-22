using TaalDc.Portal.DTO.Sales;

namespace TaalDc.Portal.ViewModels.Sales
{
    public class SalesViewModel
    {
        public SalesViewModel(IEnumerable<PaymentDTO> payments, int orderId)
        {
            Payments = payments;
            OrderId = orderId;
        }


        public IEnumerable<PaymentDTO> Payments { get; private set; }
        public int OrderId { get; private set; }
    }
}
