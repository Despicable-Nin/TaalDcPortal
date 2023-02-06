using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddPayment;

public class AddPaymentCommand : IRequest<CommandResult>
{
    public AddPaymentCommand(int transactionId, string paymentMethod, decimal amountPaid, string remarks,
        string confirmationNumber, int transactionTypeId, int paymentTypeId, DateTime paymentDate, string correlationId)
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

    public int TransactionId { get; }
    public string PaymentMethod { get; }
    public decimal AmountPaid { get; }
    public string Remarks { get; }
    public string ConfirmationNumber { get; }
    public int TransactionTypeId { get; }
    public int PaymentTypeId { get; }
    public DateTime PaymentDate { get; }

    public string CorrelationId { get; }
}