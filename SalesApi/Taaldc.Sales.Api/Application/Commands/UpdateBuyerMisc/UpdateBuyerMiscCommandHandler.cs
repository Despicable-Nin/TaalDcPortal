using MediatR;
using Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerMisc;

public class UpdateBuyerMiscCommandHandler : IRequestHandler<UpdateBuyerMiscCommand, CommandResult>
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly ILogger<UpdateBuyerMiscCommandHandler> _logger;

    public UpdateBuyerMiscCommandHandler(IBuyerRepository buyerRepository, ILogger<UpdateBuyerMiscCommandHandler> logger)
    {
        _buyerRepository = buyerRepository;
        _logger = logger;
    }
    
    public async Task<CommandResult> Handle(UpdateBuyerMiscCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Update Buyer's msic information.");

            _logger.LogInformation($"Searching for Buyer with Id: {request.BuyerId}");
            var buyer = await _buyerRepository.GetByIdAsync(request.BuyerId);

            if (buyer == default)
            {
                throw new SalesDomainException(nameof(UpdateBuyerAddressCommandHandler.Handle),
                    new Exception($"Buyer with id:{request.BuyerId} not found."));
            }
            
            buyer.UpdateMiscInformation(request.Occupation,request.Tin,request.GovIssuedId,request.GovIssuedIdValidUntil);

            _buyerRepository.Upsert(buyer);

            return CommandResult.Success(buyer.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(Handle),new object[]{request, ex.InnerException});
            throw ex;
        }
    }
}