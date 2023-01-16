namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record OrderDTO
{
    public OrderDTO(string orderId, string code, string broker, decimal finalPrice, bool isRefundable, string status, int statusId, string remarks)
    {
        OrderId = orderId;
        Code = code;
        Broker = broker;
        FinalPrice = finalPrice;
        IsRefundable = isRefundable;
        Status = status;
        StatusId = statusId;
        Remarks = remarks;
    }

    public string OrderId { get; private set; }
    public string Code { get; private set; }
    public string Broker { get; private set; }
    public decimal FinalPrice { get; private set; }
    public bool IsRefundable { get; private set; }
    public string Status { get; private set; }
    public int StatusId { get; private set; }
    public string Remarks { get; private set; }
}