using MediatR;
using SeedWork;
using Taaldc.Sales.Api.Application.Commands.SellUnit;
using Taaldc.Sales.API.Application.Commands;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Application.Commands.VoidPayment
{
    public class VoidPaymentHandler : IRequestHandler<VoidPaymentCommand, CommandResult>
    {
        private readonly IOrderRepository _repository;
        private readonly IAmCurrentUser _currentUser;
        private readonly IMediator _mediator;

        public VoidPaymentHandler(IOrderRepository repository, IAmCurrentUser currentUser, IMediator mediator)
        {
            _repository = repository;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(VoidPaymentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.Roles.Contains("ADMIN")) return CommandResult.Failed(request.PaymentId, "Unauthorized.");

            var order = await _repository.FindOrderByIdAsync(request.OrderId);

            try { 
                order.VoidPayment(request.PaymentId, _currentUser?.Email);

                _repository.UpdateOrder(order);

                await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return CommandResult.Success(request.PaymentId);
            }catch(Exception ex)
            {
                return CommandResult.Failed(request.PaymentId, ex.InnerException.Message);
            }
        }
    }
}
