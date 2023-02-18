using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public class AddBuyerCommandValidator : AbstractValidator<AddBuyerCommand>
{
    public AddBuyerCommandValidator()
    {
        RuleFor(i => i.HomeAddress).NotEmpty();
        RuleFor(i => i.FirstName).NotEmpty();
        RuleFor(i => i.LastName).NotEmpty();
        RuleFor(i => i.EmailAddress).EmailAddress().NotEmpty();
        RuleFor(i => i.CivilStatusId).NotEmpty().NotEqual(0);
        RuleFor(i => i.DoB).NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(-100))
            .LessThanOrEqualTo(DateTime.Now);
    }
}