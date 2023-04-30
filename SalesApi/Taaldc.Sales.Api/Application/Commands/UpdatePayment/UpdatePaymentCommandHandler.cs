using MediatR;
using SeedWork;
using Taaldc.Sales.Api.Application.Commands.UpdatePayment;
using Taaldc.Sales.API.Application.Commands;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, CommandResult>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IOrderRepository _repository;

    public UpdatePaymentCommandHandler(IAmCurrentUser currentUser, IOrderRepository repository)
    {
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CommandResult> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {

        var order = await _repository.FindOrderByIdAsync(request.OrderId);
        if (order == null)
        {
            if (order == default)
                throw new SalesDomainException(nameof(UpdatePaymentCommandHandler),
                    new Exception("Order not found."));
        }

        var payment = order.Payments.SingleOrDefault(i => i.Id == request.PaymentId);
        if (payment == null)
        {
            throw new SalesDomainException(nameof(UpdatePaymentCommandHandler),
                   new Exception("Payment not found."));
        }

        payment.OverridePayment(request.PaymentTypeId, request.TransactionTypeId, request.PaymentDate, request.ConfirmationNumber, request.PaymentMethod, request.AmountPaid, request.Remarks, request.CorrelationId, _currentUser.Email);

        
        return CommandResult.Success(request.OrderId);
    }
}
