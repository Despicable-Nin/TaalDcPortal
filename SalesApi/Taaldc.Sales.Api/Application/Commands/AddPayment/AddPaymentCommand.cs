using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddPayment;

public class AddPaymentCommand : IRequest<CommandResult>
{
    public AddPaymentCommand(int orderId, string paymentMethod, decimal amountPaid, string remarks,
        string confirmationNumber, int transactionTypeId, int paymentTypeId, DateTime paymentDate, string correlationId)
    {
        OrderId = orderId;
        PaymentMethod = paymentMethod;
        AmountPaid = amountPaid;
        Remarks = remarks;
        ConfirmationNumber = confirmationNumber;
        TransactionTypeId = transactionTypeId;
        PaymentTypeId = paymentTypeId;
        PaymentDate = paymentDate;
        CorrelationId = correlationId;
    }

    public int OrderId { get; init; }
    public string PaymentMethod { get; init; }
    public decimal AmountPaid { get; init; }
    public string Remarks { get; init; }
    public string ConfirmationNumber { get; init; }
    public int TransactionTypeId { get; init; }
    public int PaymentTypeId { get; init; }
    public DateTime PaymentDate { get; init; }

    public string CorrelationId { get; init; }
}