using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommandValidator : AbstractValidator<SellUnitCommand>
{
    public SellUnitCommandValidator()
    {
        RuleFor(i => i.Broker).NotEmpty();
        RuleFor(i => i.PaymentMethod).NotEmpty();
    }
}