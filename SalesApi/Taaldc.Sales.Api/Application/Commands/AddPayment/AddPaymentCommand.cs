using System.Runtime.InteropServices;
using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddPayment;

public class AddPaymentCommand : IRequest<CommandResult>
{
    public AddPaymentCommand(int transactionId, string paymentMethod, decimal amountPaid, string remarks, string confirmationNumber, int transactionTypeId, int paymentTypeId, DateTime paymentDate, string correlationId)
    {
        TransactionId = transactionId;
        PaymentMethod = paymentMethod;
        AmountPaid = amountPaid;
        Remarks = remarks;
        ConfirmationNumber = confirmationNumber;
        TransactionTypeId = transactionTypeId;
        PaymentTypeId = paymentTypeId;
        PaymentDate = paymentDate;
        CorrelationId = correlationId;
    }

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