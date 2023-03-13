using System.Net.Mail;

namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class UpdateBuyerCompanyRequest
    {
        public UpdateBuyerCompanyRequest()
        {
        }

        public int BuyerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Industry { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; } 
        public string EmailAddress { get; set; }
        public string Tin { get; set; }
        public string SecRegNo { get; set; } 
        public string President { get; set; } 
        public string CorpSec { get; set; }
    }
}
