using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerMisc;

public class UpdateBuyerMiscCommandValidator : AbstractValidator<UpdateBuyerMiscCommand>
{
    public UpdateBuyerMiscCommandValidator()
    {
        RuleFor(i => i.BuyerId).NotEmpty();
    }
}