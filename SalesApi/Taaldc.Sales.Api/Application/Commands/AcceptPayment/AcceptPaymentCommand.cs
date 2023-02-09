using MediatR;

namespace Taaldc.Sales.API.Application.Commands.ProcessPayment;

public class AcceptPaymentCommand : IRequest<CommandResult>
{
    public AcceptPaymentCommand(int orderId, int paymentId)
    {
        OrderId = orderId;
        PaymentId = paymentId;
    }

    public int OrderId { get; }
    public int PaymentId { get; }
}