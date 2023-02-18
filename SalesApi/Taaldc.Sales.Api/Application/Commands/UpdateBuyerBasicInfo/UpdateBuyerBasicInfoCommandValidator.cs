using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerBasicInfo;

public class UpdateBuyerBasicInfoCommandValidator : AbstractValidator<UpdateBuyerBasicInfoCommand>
{
    public UpdateBuyerBasicInfoCommandValidator()
    {
        RuleFor(i => i.BuyerId).NotEmpty();
        RuleFor(i => i.FirstName).NotEmpty();
        RuleFor(i => i.LastName).NotEmpty();
        RuleFor(i => i.CivilStatusId).NotEmpty().NotEqual(0);
        RuleFor(i => i.DoB).NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(-100))
            .LessThanOrEqualTo(DateTime.Now);
    }
}