namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record OrderDTO
{
    public OrderDTO(string orderId, string code, string broker, decimal finalPrice, bool isRefundable, string status,
        int statusId, string remarks)
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

    public string OrderId { get; }
    public string Code { get; }
    public string Broker { get; }
    public decimal FinalPrice { get; }
    public bool IsRefundable { get; }
    public string Status { get; }
    public int StatusId { get; }
    public string Remarks { get; }
}