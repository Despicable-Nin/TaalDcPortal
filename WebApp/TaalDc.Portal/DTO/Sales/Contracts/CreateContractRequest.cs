namespace TaalDc.Portal.DTO.Sales.Contracts
{
    public record CreateContractRequest
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

        public IList<OrderItemDto> OrderItems { get; set; }
    }


    public record OrderItemDto
    {
        public int UnitId { get; set; }
        public double SellingPrice { get; set; }
    }
}
