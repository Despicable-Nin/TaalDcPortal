using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.AddOrUpdateOrderItem;

public class AddOrUpdateOrderItemCommandHandler : IRequestHandler<AddOrUpdateOrderItemCommand, CommandResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<AddOrUpdateOrderItemCommandHandler> _logger;

    public AddOrUpdateOrderItemCommandHandler(IOrderRepository orderRepository, ILogger<AddOrUpdateOrderItemCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(AddOrUpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindOrderByIdAsync(request.OrderId);

        if (order == default)
        {
            throw new SalesDomainException(nameof(AddOrUpdateOrderItemCommandHandler),
                new Exception("Order not found."));
        }

        order.AddOrUpdateOrderItem(request.UnitId, request.Price, request.OrderItemId, request.Discount);

        _orderRepository.Update(order);

        return CommandResult.Success(order.Id);
    }
}