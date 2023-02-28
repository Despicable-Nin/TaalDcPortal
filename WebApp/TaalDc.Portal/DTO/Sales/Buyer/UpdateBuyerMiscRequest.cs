namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class UpdateBuyerMiscRequest
    {
        public int BuyerId { get; init; }
        public string Occupation { get; init; }
        public string TIN { get; init; }
        public string GovIssuedID { get; init; }
        public DateTime GovIssuedIDValidUntil { get; init; }
    }
}
