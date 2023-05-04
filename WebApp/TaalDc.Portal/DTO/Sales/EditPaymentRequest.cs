namespace TaalDc.Portal.DTO.Sales
{
    public class EditPaymentRequest
    {
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string Remarks { get; set; }
        public string ConfirmationNumber { get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string VerifiedBy { get; set; }

        public string CorrelationId { get; set; }
    }
}
