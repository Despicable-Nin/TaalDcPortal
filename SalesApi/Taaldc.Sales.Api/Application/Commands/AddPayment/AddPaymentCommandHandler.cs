using MediatR;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.AddPayment;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, CommandResult>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IOrderRepository _repository;

    public AddPaymentCommandHandler(IOrderRepository orderRepository, IAmCurrentUser currentUser)
    {
        _repository = orderRepository;
        _currentUser = currentUser;
    }

    public async Task<CommandResult> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.FindOrderByIdAsync(request.TransactionId);

        if (order == default)
            throw new SalesDomainException(nameof(AddPaymentCommandHandler),
                new Exception("Order not found."));

        var payment = order.AddPayment(
            request.PaymentTypeId,
            request.TransactionTypeId,
            request.PaymentDate,
            request.ConfirmationNumber,
            request.PaymentMethod,
            request.AmountPaid,
            request.Remarks,
            request.CorrelationId);

        _repository.UpdateOrder(order);

        return CommandResult.Success(payment.Id);
    }
}