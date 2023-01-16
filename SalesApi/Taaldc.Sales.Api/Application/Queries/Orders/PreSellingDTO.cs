namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record PreSellingDTO
{
    public PreSellingDTO(BuyerDTO buyer, OrderDTO order, UnitDTO unitR)
    {
        Buyer = buyer;
        Order = order;
        UnitR = unitR;
    }

    public BuyerDTO Buyer { get; private set; }
    public OrderDTO Order { get; private set; }
    public UnitDTO UnitR { get; private set; }
}