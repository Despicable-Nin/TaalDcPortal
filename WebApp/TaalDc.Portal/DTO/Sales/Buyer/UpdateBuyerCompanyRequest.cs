namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class UpdateBuyerCompanyRequest
    {
        public UpdateBuyerCompanyRequest()
        {
        }

        public int BuyerId { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string Industry { get; init; }
        public string PhoneNo { get; init; }
        public string MobileNo { get; init; }
        public string FaxNo { get; init; }
        public string EmailAddress { get; init; }
        public string Tin { get; init; }
        public string SecRegNo { get; init; }
        public string President { get; init; }
        public string CorpSec { get; init; }
    }
}
