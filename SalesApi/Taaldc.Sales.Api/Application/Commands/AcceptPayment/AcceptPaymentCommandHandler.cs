using MediatR;
using SeedWork;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.AcceptPayment;

public class AcceptPaymentCommandHandler : IRequestHandler<AcceptPaymentCommand, CommandResult>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IMediator _mediator;
    private readonly IOrderRepository _repository;
    private readonly IOrderQueries _orderQueries;

    public AcceptPaymentCommandHandler(
        IOrderQueries orderQueries,
        IOrderRepository repository, 
        IAmCurrentUser currentUser, 
        IMediator mediator)
    {
        _orderQueries = orderQueries;
        _repository = repository;
        _currentUser = currentUser;
        _mediator = mediator;
    }


    public async Task<CommandResult> Handle(AcceptPaymentCommand request, CancellationToken cancellationToken)
    {
       
        if (!_currentUser.Roles.Contains("ADMIN")) return CommandResult.Failed(request.PaymentId, "Unauthorized.");

        var order = await _repository.FindOrderByIdAsync(request.OrderId);

        order.AcceptPayment(request.PaymentId, _currentUser?.Email);

        _repository.Update(order);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        //update Units On Catalog --> Catalog then Replies to update UnitReplica
        int unitStatusId = 3;
        string unitStatus = "";

        var paymnent = order.Payments.FirstOrDefault(i => i.Id == request.PaymentId);

        if (paymnent.GetPaymentTypeId() == 3)
        {
            unitStatusId = 2;
            unitStatus = "SOLD";
        }
        else if (paymnent.GetPaymentTypeId() <= 2)
        {
            unitStatusId = 3;
            unitStatus = "RESERVED";
        }

        //await _mediator.Publish(new UpdateUnitReplicaStatusNotif(order.GetUnitId(), unitStatusId, unitStatus));

        return CommandResult.Success(request.PaymentId);
    }
}