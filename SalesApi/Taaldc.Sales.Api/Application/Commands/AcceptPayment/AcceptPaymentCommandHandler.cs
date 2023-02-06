using MediatR;
using SeedWork;
using Taaldc.Sales.Api.Application.Commands.SellUnit;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.ProcessPayment;

public class AcceptPaymentCommandHandler : IRequestHandler<AcceptPaymentCommand, CommandResult>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IMediator _mediator;
    private readonly IOrderRepository _repository;

    public AcceptPaymentCommandHandler(IOrderRepository repository, IAmCurrentUser currentUser, IMediator mediator)
    {
        _repository = repository;
        _currentUser = currentUser;
        _mediator = mediator;
    }


    public async Task<CommandResult> Handle(AcceptPaymentCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUser.Roles.Contains("ADMIN")) return CommandResult.Failed(request.PaymentId, "Unauthorized.");

        var order = await _repository.FindOrderByIdAsync(request.OrderId);

        order.AcceptPayment(request.PaymentId, _currentUser?.Email);

        _repository.UpdateOrder(order);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new UpdateUnitReplicaStatusNotif(order.GetUnitId, 2, "SOLD"));

        return CommandResult.Success(request.PaymentId);
    }
}