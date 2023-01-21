using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.ProcessPayment;

public class AcceptPaymentCommandHandler : IRequestHandler<AcceptPaymentCommand, CommandResult>
{
    private readonly IOrderRepository _repository;
    private readonly IAmCurrentUser _currentUser;


    public AcceptPaymentCommandHandler(IOrderRepository repository, IAmCurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

   
    public async Task<CommandResult> Handle(AcceptPaymentCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUser.Roles.Contains("admin")) return CommandResult.Failed(request.PaymentId, "Unauthorized.");
        
        await _repository.AcceptPayment(request.OrderId, request.PaymentId, _currentUser.Email);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success(request.PaymentId);
    }
}