using MediatR;
using Taaldc.Sales.API.Application.Commands.UpdateBuyerMisc;
using Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure.Repositories;

namespace Taaldc.Sales.API.Application.Commands.UpsertCompany;

public class UpsertCompanyCommandHandler : IRequestHandler<UpsertCompanyCommand, bool>
{
    private readonly IBuyerRepository _repository;
    private readonly ILogger<UpsertCompanyCommandHandler> _logger;

    public UpsertCompanyCommandHandler(IBuyerRepository buyerRepository, ILogger<UpsertCompanyCommandHandler> logger)
    {
        _repository = buyerRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpsertCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Searching for Buyer with Id: {request.BuyerId}");
            var buyer = await _repository.GetByIdAsync(request.BuyerId);

            if (buyer == default)
            {
                throw new SalesDomainException(nameof(UpsertCompanyCommandHandler.Handle),
                    new Exception($"Buyer with id:{request.BuyerId} not found."));
            }
            
            _logger.LogInformation("Upsert Company to Buyer.");
            _logger.LogInformation($"Company is replaced if exists.");
            buyer.UpsertCompany(request.ToEntity());
            
            _repository.Upsert(buyer);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,nameof(UpsertCompanyCommandHandler.Handle), new object[]{request, ex.InnerException});
            throw;
        }
    }
}