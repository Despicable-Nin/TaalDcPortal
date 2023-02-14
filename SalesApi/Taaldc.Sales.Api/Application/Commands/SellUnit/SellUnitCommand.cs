using MediatR;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommand : IRequest<SellUnitCommandResult>
{
    public SellUnitCommand(int buyerId, string broker,string paymentMethod, int paymentReferenceId, decimal reservationFee, string reservationConfirmation, decimal downpayment, string downpaymentConfirmation, decimal discount, string remarks, ICollection<OrderItemDTO> orderItems)
    {
        BuyerId = buyerId;
        Broker = broker;
        PaymentMethod = paymentMethod;
        PaymentReferenceId = paymentReferenceId;
        ReservationFee = reservationFee;
        ReservationConfirmation = reservationConfirmation;
        Downpayment = downpayment;
        DownpaymentConfirmation = downpaymentConfirmation;
        Discount = discount;
        Remarks = remarks;
        OrderItems = orderItems ?? new List<OrderItemDTO>();
    }

    public int BuyerId { get; private set; }
    
    public string Broker { get; private set; }

    public int PaymentReferenceId { get; private set; }

    public DateTime TransactionDate { get; private set; } = DateTime.Now;

    public decimal ReservationFee { get; private set; }
    public string ReservationConfirmation { get; private set; }

    public decimal Downpayment { get; private set; }
    public string DownpaymentConfirmation { get; private set; }
    
    public decimal Discount { get; private set; }
    public string Remarks { get; private set; }
    
    public ICollection<OrderItemDTO> OrderItems { get; private set; }
    public string PaymentMethod { get; private set; }
}

public class OrderItemDTO
{
    public int UnitId { get; private set; }
    public decimal Price { get; private set; }
}