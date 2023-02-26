namespace TaalDc.Portal.DTO.Sales.Contracts
{
    public class ContractCreate_ClientDto
    {
        public int BuyerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Broker { get; set; }
        
        public double Reservation { get; set; }
        public string ReservationConfirmationNo { get; set; }
        public double DownPayment { get; set; }
        public string DownPaymentConfirmationNo { get; set; }

        public string PaymentMethod { get; set; }
        public string Remarks { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }


    public class OrderItem
    {
        public int UnitId { get; set; }
        public double Price { get; set; }
    }
}
