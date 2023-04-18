using MediatR;
using Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerBasicInfo;

public class UpdateBuyerBasicInfoCommand : IRequest<CommandResult>
{
    public int BuyerId { get; init; }
    public string Salutation { get; init; }
    public string FirstName { get; init; }
    public string MiddleName { get; init; }
    public string LastName { get; init; }
    public DateTime DoB { get; init; }
    public int CivilStatusId { get; init; }
    public bool IsCorporate { get; init; }
}

public class UpdateBuyerBasicInfoCommandHandler : IRequestHandler<UpdateBuyerBasicInfoCommand, CommandResult>
{
    
    private readonly IBuyerRepository    _buyerRepository;
    private readonly ILogger<UpdateBuyerBasicInfoCommandHandler> _logger;

    public UpdateBuyerBasicInfoCommandHandler(IBuyerRepository repository, ILogger<UpdateBuyerBasicInfoCommandHandler> logger)
    {
        _buyerRepository = repository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(UpdateBuyerBasicInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Update Buyer's basic information.");

            _logger.LogInformation($"Searching for Buyer with Id: {request.BuyerId}");
            var buyer = await _buyerRepository.GetByIdAsync(request.BuyerId);

            if (buyer == default)
            {
                throw new SalesDomainException(nameof(UpdateBuyerAddressCommandHandler.Handle),
                    new Exception($"Buyer with id:{request.BuyerId} not found."));
            }

            _logger.LogInformation($"Update Buyer's basic info...");
            buyer.UpdateBasicInfo(
                request.Salutation, 
                request.FirstName, 
                request.MiddleName, 
                request.LastName,
                request.DoB, 
                request.CivilStatusId, 
                request.IsCorporate);

            if(request.CivilStatusId != (int)CivilStatus.CivilStatusIs.Married)
            {
                buyer.AddPartnerOrSpouse(0);
            }
            
            _buyerRepository.Upsert(buyer);

            return CommandResult.Success(buyer.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,nameof(UpdateBuyerBasicInfoCommandHandler.Handle),new object[]{request, ex.InnerException});
            throw;
        }
    }
}

