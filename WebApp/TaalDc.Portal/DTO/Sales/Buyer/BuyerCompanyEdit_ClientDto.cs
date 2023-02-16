namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class BuyerCompanyEdit_ClientDto
    {
        public int BuyerId { get; set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Industry { get; private set; }
        public string PhoneNo { get; private set; }
        public string MobileNo { get; private set; }
        public string FaxNo { get; private set; }
        public string EmailAddress { get; private set; }
        public string TIN { get; private set; }
        public string SECRegNo { get; private set; }
        public string President { get; private set; }
        public string CorpSec { get; private set; }
    }
}
