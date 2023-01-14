using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommandValidator : AbstractValidator<SellUnitCommand>
{
    public SellUnitCommandValidator()
    {
        RuleFor(i => i.Broker).NotEmpty();
        RuleFor(i => i.FirstName).NotEmpty();
        RuleFor(i => i.LastName).NotEmpty();

        RuleFor(i => i.EmailAddress).EmailAddress().NotEmpty();
        RuleFor(i => i.ContactNo).NotEmpty();

        RuleFor(i => i.ZipCode).NotEmpty();
        RuleFor(i => i.Reservation).NotEmpty();
        RuleFor(i => i.PaymentDate).NotEmpty();
        RuleFor(i => i.PaymentMethod).NotEmpty();
    }
}