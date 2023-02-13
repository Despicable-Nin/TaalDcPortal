using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class OrderItem : DomainEntity
{
    protected OrderItem(){}



    public OrderItem(int unitId, decimal discount, decimal price)
    {
        _unitId = unitId;
        Discount = discount;
        Price = price;
    }

    private int _unitId;
    public int GetUnitId() => _unitId;
    public decimal Discount { get; private set; }
    public decimal Price { get; private set; }
}