namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class UpdateBuyerContactRequest
    {
        public int BuyerId { get; init; }
        public string EmailAddress { get; init; }
        public string MobileNo { get; init; }
        public string PhoneNo { get; init; }

    }
}
