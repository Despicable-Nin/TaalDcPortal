using MediatR;
using SeedWork;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Events;

namespace Taaldc.Sales.API.Application.Commands.AcceptPayment;

public class AcceptPaymentCommandHandler : IRequestHandler<AcceptPaymentCommand, CommandResult>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IMediator _mediator;
    private readonly IOrderRepository _repository;
    private readonly IOrderQueries _orderQueries;
    private readonly IUnitReplicaRepository _unitRepository;

    public AcceptPaymentCommandHandler(
        IOrderQueries orderQueries,
        IOrderRepository repository, 
        IAmCurrentUser currentUser, 
        IMediator mediator,
        IUnitReplicaRepository unitRepository)
    {
        _orderQueries = orderQueries;
        _repository = repository;
        _currentUser = currentUser;
        _mediator = mediator;
        _unitRepository = unitRepository;
    }


    public async Task<CommandResult> Handle(AcceptPaymentCommand request, CancellationToken cancellationToken)
    {
       
        if (!_currentUser.Roles.Contains("ADMIN")) return CommandResult.Failed(request.PaymentId, "Unauthorized.");

        var order = await _repository.FindOrderByIdAsync(request.OrderId);

        order.AcceptPayment(request.PaymentId, _currentUser?.Email, request.ConfirmationNumber);

        _repository.Update(order);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        //update Units On Catalog --> Catalog then Replies to update UnitReplica
        int unitStatusId = 3;
        string unitStatus = "";

        var paymnent = order.Payments.FirstOrDefault(i => i.Id == request.PaymentId);

        var markUnitAsSold = order.Payments.Where(i => i.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Accepted)).Sum(i => i.AmountPaid) >= (order.OrderItems.Sum(i => i.Price) * 0.10M);

        if (paymnent.GetPaymentTypeId() == 3 || markUnitAsSold)
        {
            unitStatusId = 2;
            unitStatus = "SOLD";
        }
        else if (paymnent.GetPaymentTypeId() <= 2)
        {
            unitStatusId = 3;
            unitStatus = "RESERVED";
        }

        //Update unit status
        foreach (var item in order.OrderItems)
        {
            order.AddDomainEvent(new UnitReplicaStatusChangedToReservedDomainEvent(item.GetUnitId(), unitStatusId, unitStatus));
            
            //await _unitRepository.SyncUnitStatusWithCatalog(item.GetUnitId(), unitStatusId); 
            //await _unitRepository.UnitOfWork.SaveChangesAsync();
            //await _mediator.Publish(new UnitReplicaStatusChangedToReservedDomainEvent(item.GetUnitId(), unitStatusId, unitStatus));
        }


        

        return CommandResult.Success(request.PaymentId);
    }
}