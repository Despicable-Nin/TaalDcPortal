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
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeState { get; set; }
        public string HomeCountry { get; set; }
        public string HomeZipCode { get; set; }
        public bool IsCorporate { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyIndustry { get; set; } = string.Empty;
    }


    public class BuyerCreateAPI_ClientDto
    {
        public BuyerCreateAPI_ClientDto(
            string salutation,
            string firstName,
            string middleName,
            string lastName,
            string emailAddress,
            string phoneNo,
            string mobileNo,
            DateTime doB,
            int civilStatusId,
            AddressDto address,
            bool isCorporate,
            CompanyDto company)
        {
            Salutation = salutation;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNo = phoneNo;
            MobileNo = mobileNo;
            DoB = doB;
            CivilStatusId = civilStatusId;
            HomeAddress = address;
            Company = company;
        }

        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DoB { get; set; }
        public int CivilStatusId { get; set; }
        public bool IsCorporate { get; set; }
        //value object
        public CompanyDto Company { get; set; }
        public AddressDto HomeAddress { get; set; }
    }

    public record AddressDto
    {
        public AddressDto(string street, 
            string city,
            string state,
            string country,
            string zipCode)
        {
            Street = !string.IsNullOrEmpty(street) ? street : "";
            City = !string.IsNullOrEmpty(city) ? city : "";
            State = !string.IsNullOrEmpty(state) ? state : "";
            Country = !string.IsNullOrEmpty(country) ? country : "";
            ZipCode = !string.IsNullOrEmpty(zipCode) ? zipCode : "";
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public record CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(string name
            ,string address
            ,string industry
            ,string phoneNo
            ,string mobileNo
            ,string faxNo
            ,string emailAddress
            ,string tin
            ,string secRegNo
            ,string president
            ,string corpSec)
        {
            Name = !string.IsNullOrEmpty(name) ? name: "";
            Address = !string.IsNullOrEmpty(address)? address: "";
            Industry = !string.IsNullOrEmpty(industry) ? industry : "";
            PhoneNo = !string.IsNullOrEmpty(phoneNo) ? phoneNo : "";
            MobileNo = !string.IsNullOrEmpty(mobileNo) ? mobileNo : "";
            FaxNo = !string.IsNullOrEmpty(faxNo) ? faxNo : "";
            EmailAddress = !string.IsNullOrEmpty(emailAddress) ? emailAddress : "";
            Tin= !string.IsNullOrEmpty(tin) ? tin : "";
            SecRegNo = !string.IsNullOrEmpty(secRegNo) ? secRegNo : "";
            President = !string.IsNullOrEmpty(president) ? president : "";
            CorpSec = !string.IsNullOrEmpty(corpSec) ? corpSec : "";
        }

        public string Name { get; private set; }
        public string Address { get; private set; } 
        public string Industry { get; private set; }
        public string PhoneNo { get; private set; }
        public string MobileNo { get; private set; }
        public string FaxNo { get; private set; }
        public string EmailAddress { get; private set; }
        public string Tin { get; private set; }
        public string SecRegNo { get; private set; }
        public string President { get; private set; }
        public string CorpSec { get; private set; }
    }
}

