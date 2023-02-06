using FluentValidation;
using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public class AddBuyerCommand : IRequest<int>
{
    
}

public class AddBuyerCommandHandler : IRequestHandler<AddBuyerCommand, int>
{
    public Task<int> Handle(AddBuyerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class AddBuyerCommandValidator : AbstractValidator<AddBuyerCommand>
{
    public AddBuyerCommandValidator()
    {
        //RuleFor()
    }
}