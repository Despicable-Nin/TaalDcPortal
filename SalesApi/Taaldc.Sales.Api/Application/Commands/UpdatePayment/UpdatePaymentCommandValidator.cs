using FluentValidation;
using Taaldc.Sales.Api.Application.Commands.UpdatePayment;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        RuleFor(i => i.OrderId).NotEmpty();
        RuleFor(i => i.PaymentId).NotEmpty();
        RuleFor(i => i.PaymentMethod).NotEmpty();
        RuleFor(i => i.AmountPaid).NotEmpty();
        RuleFor(i => i.Remarks).NotEmpty();
        RuleFor(i => i.ConfirmationNumber).NotEmpty();
        RuleFor(i => i.TransactionTypeId).NotEmpty();
        RuleFor(i => i.PaymentTypeId).NotEmpty();
        RuleFor(i => i.PaymentDate).NotEmpty();

    }
}
