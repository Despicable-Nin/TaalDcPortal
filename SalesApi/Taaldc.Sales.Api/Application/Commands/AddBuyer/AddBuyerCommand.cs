using FluentValidation;
using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public class AddBuyerCommand : IRequest<int>
{
    public string Salutation { get; }

    public string FirstName { get; }

    public string MiddleName { get; }

    public string LastName { get; }

    public DateTime DoB { get; }
    public int CivilStatusId { get; set; }

    public string EmailAddress { get; }

    public string PhoneNo { get; }
    public string MobileNo { get; }

    public string Address { get; }
    public string Country { get; }

    public string Province { get; }

    public string TownCity { get; }

    public string ZipCode { get; }
}

public class AddBuyerCommandHandler : IRequestHandler<AddBuyerCommand, int>
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly ILogger<AddBuyerCommandHandler> _logger;
    
    public async Task<int> Handle(AddBuyerCommand request, CancellationToken cancellationToken)
    {
        var buyer = _buyerRepository.GetByEmail(request.EmailAddress);

        var buyerId = buyer?.Id;

        //upsert buyer
        buyer = _buyerRepository.Upsert(
            request.Salutation,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.DoB,
            request.CivilStatusId,
            request.EmailAddress,
            request.PhoneNo,
            request.MobileNo,
            buyerId);

        //persist to database
        await _buyerRepository.UnitOfWork.SaveChangesAsync();

        return buyer.Id;
    }
}

public class AddBuyerCommandValidator : AbstractValidator<AddBuyerCommand>
{
    public AddBuyerCommandValidator()
    {
        RuleFor(i => i.FirstName).NotEmpty();
        RuleFor(i => i.LastName).NotEmpty();
        RuleFor(i => i.EmailAddress).EmailAddress();
        RuleFor(i => i.ZipCode).NotEmpty();
        
    }
}