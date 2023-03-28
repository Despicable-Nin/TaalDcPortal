namespace TaalDc.Portal.DTO.Sales.Contracts
{
    public record CreateContractRequest
    {
        public CreateContractRequest()
        {
            Broker = String.Empty;
        }

        public int BuyerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Broker { get; set; }
        
        public double ReservationFee { get; set; }
        public string ReservationConfirmation { get; set; }
        public double DownPayment { get; set; }
        public string DownpaymentConfirmation { get; set; }

        public string PaymentMethod { get; set; }
        public string Remarks { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }
    }


    public record OrderItemDto
    {
        public int UnitId { get; set; }
        public double Price { get; set; }
    }
}
