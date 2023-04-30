using MediatR;
using Taaldc.Sales.API.Application.Commands;
using Taaldc.Sales.API.Application.Commands.AddPayment;

namespace Taaldc.Sales.Api.Application.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest<CommandResult>
    {
        public int OrderId { get; init; }
        public int PaymentId { get; init; }
        public string PaymentMethod { get; init; }
        public decimal AmountPaid { get; init; }
        public string Remarks { get; init; }
        public string ConfirmationNumber { get; init; }
        public int TransactionTypeId { get; init; }
        public int PaymentTypeId { get; init; }
        public DateTime PaymentDate { get; init; }
        public string VerifiedBy { get; init; }

        public string CorrelationId { get; init; }
    }
}
