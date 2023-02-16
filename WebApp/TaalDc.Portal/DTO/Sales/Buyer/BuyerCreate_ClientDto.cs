using System;

namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class BuyerCreate_ClientDto
    {
        public BuyerCreate_ClientDto()
        {
        }


        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeState { get; set; }
        public string HomeCountry { get; set; }
        public string HomeZipCode { get; set; }
        public bool IsCorporate { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyIndustry { get; set; }
    }
}

