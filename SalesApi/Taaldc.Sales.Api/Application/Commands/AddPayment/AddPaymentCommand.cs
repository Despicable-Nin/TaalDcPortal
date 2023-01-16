using System.Runtime.InteropServices;
using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddPayment;

public class AddPaymentCommand : IRequest<CommandResult>
{
    public int TransactionId { get; private set; }
    public string PaymentMethod { get; private set; }
    public decimal AmountPaid { get; private set; }
    public string Remarks { get; private set; }
    public string ConfirmationNumber { get; private set; }
    public int TransactionTypeId { get; private set; }
    public int PaymentTypeId { get; private set; }
    public DateTime PaymentDate { get; private set; }

    public string CorrelationId { get; private set; }
}