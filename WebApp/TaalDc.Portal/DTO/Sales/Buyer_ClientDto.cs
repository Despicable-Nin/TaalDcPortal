using System;
using TaalDc.Portal.DTO.Enums;

namespace TaalDc.Portal.DTO.Sales
{
    public class Buyer_ClientDto
    {
        public Buyer_ClientDto(string salutation, string firstName, string middleName, string lastName, DateTime doB, CivilStatusEnum civilStatus)
        {
            Salutation = salutation;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DoB = doB;
            CivilStatusId = 1;
            CivilStatus = "Single";
        }

        public int Id { get; set; }

        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public int CivilStatusId { get; set; }
        public string CivilStatus { get; set; }


        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }

        public string Occupation { get; set; }
        public string TIN { get; set; }
        public string GovIssuedID { get; set; }
        public DateTime GovIssuedIDValidUntil { get; set; }

        public int? SpouseId { get; set; }
        public BuyerCreate_ClientDto? Spouse { get; set; }

        public ClientAddress HomeAddress { get; set; }
        public ClientAddress BusinessAddress { get; set; }
        public ClientAddress BillingAddress { get; set; }

        public Company Company { get; set; }
        public bool IsCorporate { get; set; }


        public void SetContactDetails(string emailAddress, string mobileNo, string phoneNo)
        {
            EmailAddress = emailAddress;
            MobileNo = mobileNo;
            PhoneNo = phoneNo;
        }

        public void SetIDInformation(string occupation, string tin, string govIssuedID, DateTime govIssuedIDValidUntil)
        {
            Occupation = occupation;
            TIN = tin;
            GovIssuedID = govIssuedID;
            GovIssuedIDValidUntil = govIssuedIDValidUntil;
        }
    }

    public record Company
    {
        public Company(string name, string address, string industry)
        {
            Name = name;
            Address = address;
            Industry = industry;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Industry { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string TIN { get; set; }
        public string SECRegNo { get; set; }
        public string President { get; set; }
        public string CorpSec { get; set; }
    }

    public record ClientAddress
    {
        public ClientAddress(string address, string city, string state, string country, string zipCode)
        {
            Street = address;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}

