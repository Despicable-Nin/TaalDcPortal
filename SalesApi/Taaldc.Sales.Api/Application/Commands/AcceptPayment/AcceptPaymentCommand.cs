using MediatR;

namespace Taaldc.Sales.API.Application.Commands.ProcessPayment;

public class AcceptPaymentCommand : IRequest<CommandResult>
{
    public AcceptPaymentCommand(int paymentId)
    {
        PaymentId = paymentId;
    }

    public int PaymentId { get; private set; }
}