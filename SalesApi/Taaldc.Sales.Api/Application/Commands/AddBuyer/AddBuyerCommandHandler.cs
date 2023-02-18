using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public class AddBuyerCommandHandler : IRequestHandler<AddBuyerCommand, int>
{

    private readonly IBuyerRepository _buyerRepository;
    private readonly ILogger<AddBuyerCommandHandler> _logger;

    public AddBuyerCommandHandler(IBuyerRepository buyerRepository, ILogger<AddBuyerCommandHandler> logger)
    {
        _buyerRepository = buyerRepository;
        _logger = logger;
    }


    public async Task<int> Handle(AddBuyerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start adding 'Buyer' entity.");
            Buyer buyer = default; //default = null, it's type safe

            _logger.LogInformation($"'Check if Buyer's email has been used.");
            buyer = await _buyerRepository.GetByEmailAsync(request.EmailAddress);

            if (buyer != default)
            {
                throw new SalesDomainException(nameof(AddBuyerCommandHandler),
                    new Exception("Email has already been registered."));
            }

            buyer = new Buyer(request.Salutation, request.FirstName, request.MiddleName, request.LastName,
                request.EmailAddress, request.PhoneNo, request.MobileNo, request.DoB, request.CivilStatusId, null,
                request.IsCorporate,
                null);

            _logger.LogInformation($"Upsert address -- replace if same type exists,otherwise just add.");
            buyer.UpsertAddress(request.HomeAddress.ToEntity());

            _logger.LogInformation($"Update Company -- replace if exists, otherwise just add.");
            buyer.UpdateCompany(request.Company.ToEntity());

            return request.BuyerId.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,nameof(AddBuyerCommandHandler.Handle), new object[]{request,ex.InnerException});
            throw;
        }
    }
}