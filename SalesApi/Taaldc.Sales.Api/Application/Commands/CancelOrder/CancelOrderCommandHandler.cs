using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.UpdateOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, CommandResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CancelOrderCommandHandler> _logger;


    public CancelOrderCommandHandler(IOrderRepository orderRepository, ILogger<CancelOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindOrderByIdAsync(request.OrderId);

        if (order == null)
            throw new SalesDomainException(nameof(CancelOrderCommandHandler),
                new ArgumentNullException("Order not found."));

        order.CancelOrder();

        return CommandResult.Success(order.Id);
    }
}