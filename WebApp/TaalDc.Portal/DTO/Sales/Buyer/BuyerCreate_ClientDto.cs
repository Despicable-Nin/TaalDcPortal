using System;

namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class BuyerCreate_ClientDto
    {
        public BuyerCreate_ClientDto()
        {
        }

        public string Salutation { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; } = string.Empty;
        public string LastName { get; init; }
        public string EmailAddress { get; init; }
        public string MobileNo { get; init; }
        public string HomeAddress { get; init; }
        public string HomeCity { get; init; }
        public string HomeState { get; init; }
        public string HomeCountry { get; init; }
        public string HomeZipCode { get; init; }
        public bool IsCorporate { get; init; }
        public string CompanyName { get; init; } = string.Empty;
        public string CompanyAddress { get; init; } = string.Empty;
        public string CompanyIndustry { get; init; } = string.Empty;
    }


    public class AddBuyerRequest
    {
        public AddBuyerRequest(
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

        public string Salutation { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public string EmailAddress { get; init; }
        public string MobileNo { get; init; }
        public string PhoneNo { get; init; }
        public DateTime DoB { get; init; }
        public int CivilStatusId { get; init; }
        public bool IsCorporate { get; init; }
        //value object
        public CompanyDto Company { get; init; }
        public AddressDto HomeAddress { get; init; }
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

        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public string ZipCode { get; init; }
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

        public string Name { get; private init; }
        public string Address { get; private init; } 
        public string Industry { get; private init; }
        public string PhoneNo { get; private init; }
        public string MobileNo { get; private init; }
        public string FaxNo { get; private init; }
        public string EmailAddress { get; private init; }
        public string Tin { get; private init; }
        public string SecRegNo { get; private init; }
        public string President { get; private init; }
        public string CorpSec { get; private init; }
    }
}

