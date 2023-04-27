using MediatR;

namespace Taaldc.Sales.API.Application.Commands.UpdateOrder;

public class CancelOrderCommand : IRequest<CommandResult>
{
    public CancelOrderCommand(int orderId, int statusId)
    {
        StatusId = statusId;
        OrderId = orderId;
    }
    
    public int OrderId { get; init; }
    public int StatusId { get; init; }
}