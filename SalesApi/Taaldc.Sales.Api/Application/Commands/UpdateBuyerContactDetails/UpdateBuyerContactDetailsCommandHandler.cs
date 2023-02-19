using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerContactDetails;

public class UpdateBuyerContactDetailsCommandHandler : IRequestHandler<UpdateBuyerContactDetailsCommand, bool>
{
    private readonly IBuyerRepository _repository;
    private readonly ILogger<UpdateBuyerContactDetailsCommandHandler> _logger;

    public UpdateBuyerContactDetailsCommandHandler(IBuyerRepository repository, ILogger<UpdateBuyerContactDetailsCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateBuyerContactDetailsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Update Buyer's msic information.");

            _logger.LogInformation($"Searching for Buyer with Id: {request.BuyerId}");
            var buyer = await _repository.GetByIdAsync(request.BuyerId);

            if (buyer == default)
            {
                throw new SalesDomainException(nameof(UpdateBuyerContactDetailsCommandHandler.Handle),
                    new Exception($"Buyer with id:{request.BuyerId} not found."));
            }

            buyer.UpdateContactDetails(request.EmailAddress, request.PhoneNo, request.MobileNo);

            _repository.Upsert(buyer);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(Handle),new object[]{request, ex.InnerException});
            throw;
        }
    }
}