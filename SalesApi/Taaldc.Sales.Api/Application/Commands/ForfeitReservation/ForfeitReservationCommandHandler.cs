using FluentValidation;
using MediatR;
using Taaldc.Sales.API.Application.Commands;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Application.Commands.ForfeitReservation
{
    public class ForfeitReservationCommandHandler : IRequestHandler<ForfeitReservationCommand, CommandResult>
    {
        private IOrderRepository _orderRepository;
        private ILogger<ForfeitReservationCommandHandler> _logger;

        public ForfeitReservationCommandHandler(IOrderRepository orderRepository, ILogger<ForfeitReservationCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CommandResult> Handle(ForfeitReservationCommand request, CancellationToken cancellationToken)
        {

            var order = await _orderRepository.FindOrderByIdAsync(request.OrderId) ?? 
                throw new ValidationException($"Order with Id:{request.OrderId} cannot be found.");

            order.Forfeit();

            return CommandResult.Success(request.OrderId);
        }
    }
}
