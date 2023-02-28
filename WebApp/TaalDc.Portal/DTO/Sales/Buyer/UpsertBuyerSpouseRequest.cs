namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class UpsertBuyerSpouseRequest
    {
        public int BuyerId { get; init; }
        public int SpouseId { get; init; }

        public string Salutation { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public DateTime DoB { get; init; }

        public string EmailAddress { get; init; }
        public string MobileNo { get; init; }
        public string PhoneNo { get; init; }

        public string Occupation { get; init; }
        public string TIN { get; init; }
        public string GovIssuedID { get; init; }
        public DateTime GovIssuedIDValidUntil { get; init; }
    }
}
