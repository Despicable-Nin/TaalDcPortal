using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerAddress;

public class UpdateBuyerCommmandValidator : AbstractValidator<UpdateBuyerAddressCommand>
{
    public UpdateBuyerCommmandValidator()
    {
        RuleFor(i => i.BuyerId).NotEmpty();
        RuleFor(i => i.ZipCode).NotEmpty();
        RuleFor(i => i.City).NotEmpty();
        RuleFor(i => i.State).NotEmpty();
        RuleFor(i => i.Street).NotEmpty();
        RuleFor(i => i.Type).NotEmpty();
    }
}