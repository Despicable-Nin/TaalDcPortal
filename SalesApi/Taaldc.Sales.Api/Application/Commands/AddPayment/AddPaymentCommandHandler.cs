using MediatR;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.AddPayment;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, CommandResult>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IOrderRepository _repository;
    
    public async Task<CommandResult> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var tran = await _repository.FindOrderByIdAsync(request.TransactionId);

        if (tran == default)
            throw new SalesDomainException(nameof(AddPaymentCommandHandler),
                new Exception(("Sales transaction not found.")));

        Payment payment = tran.AddPayment(request.PaymentTypeId, request.TransactionTypeId, request.PaymentDate,
            request.ConfirmationNumber, request.PaymentMethod, request.AmountPaid, request.Remarks,
            request.CorrelationId);
        
        _repository.UpdateOrder(tran);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(payment.Id);

    }
}