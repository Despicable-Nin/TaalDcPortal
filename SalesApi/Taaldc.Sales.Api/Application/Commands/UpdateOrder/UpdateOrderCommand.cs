using MediatR;

namespace Taaldc.Sales.API.Application.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<CommandResult>
{
    public UpdateOrderCommand(string broker, string remarks, DateTime transactionDate, int statusId, decimal discount)
    {
        Broker = broker;
        Remarks = remarks;
        TransactionDate = transactionDate;
        StatusId = statusId;
        Discount = discount;
    }
    
    public int OrderId { get; init; }
    public string Broker { get; init; }
    public string Remarks { get; init; }
    public decimal Discount { get; init; }
    public DateTime TransactionDate { get; init; }
    public int StatusId { get; init; }
}