using FluentValidation;

namespace Taaldc.Sales.API.Application.Commands.UpsertCompany;

public class UpsertCompanyCommandValidator : AbstractValidator<UpsertCompanyCommand>
{
    public UpsertCompanyCommandValidator()
    {
        RuleFor(i => i.BuyerId).NotEmpty();
        RuleFor(i => i.Name).NotEmpty();
        
    }
}