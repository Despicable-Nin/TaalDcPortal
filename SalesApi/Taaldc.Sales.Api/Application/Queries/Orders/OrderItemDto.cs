namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record OrderItemDto
{
    public int UnitId { get; init; }
    public double SellingPrice { get; init; }
}