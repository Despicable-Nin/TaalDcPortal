namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class BuyerSpouseUpsert_ClientDto
    {
        public int BuyerId { get; set; }
        public int SpouseId { get; set; }

        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }

        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }

        public string Occupation { get; set; }
        public string TIN { get; set; }
        public string GovIssuedID { get; set; }
        public DateTime GovIssuedIDValidUntil { get; set; }
    }
}
