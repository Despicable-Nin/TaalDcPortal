using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AcceptPayment;

public class AcceptPaymentCommand : IRequest<CommandResult>
{
    public AcceptPaymentCommand(int orderId, int paymentId, string confirmationNumber)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        ConfirmationNumber = confirmationNumber;
    }

    public int OrderId { get; }
    public int PaymentId { get; }
    public string ConfirmationNumber { get; }
}