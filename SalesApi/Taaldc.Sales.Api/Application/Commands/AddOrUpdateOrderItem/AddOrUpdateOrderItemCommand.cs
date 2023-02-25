using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddOrUpdateOrderItem;

public class AddOrUpdateOrderItemCommand : IRequest<CommandResult>
{
    public int OrderId { get; init; }
    public int? OrderItemId { get; init; }
    public int UnitId { get; init; }
    public decimal Discount { get; init; }
    public decimal Price { get; init; }
}