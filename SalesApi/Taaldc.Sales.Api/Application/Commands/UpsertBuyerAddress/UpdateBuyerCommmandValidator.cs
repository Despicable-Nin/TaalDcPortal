using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;

public class UpdateBuyerCommmandValidator : AbstractValidator<UpsertBuyerAddressCommand>
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