using MediatR;
using Taaldc.Sales.API.Application.Commands;

namespace Taaldc.Sales.Api.Application.Commands.VoidPayment
{
    public class VoidPaymentCommand : IRequest<CommandResult>
    {
        public VoidPaymentCommand(int orderId, int paymentId)
        {
            OrderId = orderId;
            PaymentId = paymentId;
        }

        public int OrderId { get; private set; }
        public int PaymentId { get; private set; }
    }
}
