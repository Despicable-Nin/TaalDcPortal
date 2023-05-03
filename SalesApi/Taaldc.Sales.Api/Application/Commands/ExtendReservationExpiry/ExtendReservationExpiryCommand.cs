using FluentValidation;
using MediatR;
using Taaldc.Sales.Api.Application.Commands.ForfeitReservation;
using Taaldc.Sales.API.Application.Commands;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Application.Commands.ExtendReservationExpiry
{
    public class ExtendReservationExpiryCommand : IRequest<CommandResult>
    {
        public ExtendReservationExpiryCommand(int orderId, int days) {
            OrderId = orderId;
            Days = days;
        }

        public int OrderId { get; }
        public int Days { get; }
    }

    public class ExtendReservationExpirtyCommandHandler : IRequestHandler<ExtendReservationExpiryCommand,CommandResult>
    {
        private IOrderRepository _orderRepository;
        private ILogger<ExtendReservationExpirtyCommandHandler> _logger;

        public ExtendReservationExpirtyCommandHandler(IOrderRepository orderRepository, ILogger<ExtendReservationExpirtyCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CommandResult> Handle(ExtendReservationExpiryCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindOrderByIdAsync(request.OrderId) ??
                throw new ValidationException($"Order with Id:{request.OrderId} cannot be found.");

            order.ExtendReservation(request.Days);

            return CommandResult.Success(request.OrderId);
        }
    }
}
