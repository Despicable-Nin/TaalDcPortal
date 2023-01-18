namespace TaalDc.Portal.ViewModels.Sales
{
    public class SalesCreateDTO
    {
        public string Code { get; set; }

        public string Broker { get; set; }

        public bool IsRefundable { get; set; }

        public int UnitId { get; set; }

        public decimal SellingPrice { get; set; }

        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string ContactNo { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string TownCity { get; set; }

        public string ZipCode { get; set; }

        public decimal Reservation { get; set; }
        public string ReservationConfirmNo { get; set; }

        public decimal DownPayment { get; set; }
        public string DownPaymentConfirmNo { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }

        public string Remarks { get; set; }
    }
}
