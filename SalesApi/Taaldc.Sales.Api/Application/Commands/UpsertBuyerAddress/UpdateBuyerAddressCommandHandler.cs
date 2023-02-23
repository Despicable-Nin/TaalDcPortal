using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;

public class UpdateBuyerAddressCommandHandler : IRequestHandler<UpsertBuyerAddressCommand, CommandResult>
{

    private readonly IBuyerRepository _repository;
    private readonly ILogger<UpdateBuyerAddressCommandHandler> _logger;

    public UpdateBuyerAddressCommandHandler(IBuyerRepository repository, ILogger<UpdateBuyerAddressCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(UpsertBuyerAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Upsert Address to Buyer.");
            _logger.LogInformation($"Address Type:{Enum.GetName(typeof(AddressTypeEnum), request.Type)}");

            _logger.LogInformation($"Searching for Buyer with Id: {request.BuyerId}");
            var buyer = await _repository.GetByIdAsync(request.BuyerId);

            if (buyer == default)
            {
                throw new SalesDomainException(nameof(UpdateBuyerAddressCommandHandler.Handle),
                    new Exception($"Buyer with id:{request.BuyerId} not found."));
            }

            _logger.LogInformation($"Upsert address -- replace if same type exists,otherwise just add.");
            buyer.UpsertAddress(
                new Address(
                    request.Type,
                    request.Street,
                    request.City,
                    request.State,
                    request.Country,
                    request.ZipCode));

            _logger.LogInformation("Saving changes invoked on transaction pipeline...");
            _repository.Upsert(buyer);
            
            return CommandResult.Success(buyer.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(UpdateBuyerAddressCommandHandler.Handle), new object [] { request, ex.InnerException });
            return CommandResult.Failed(null, ex.Message);
        }
    }
}