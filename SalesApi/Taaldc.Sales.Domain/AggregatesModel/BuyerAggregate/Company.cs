using System;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate
{
    public class Company : ValueObject
    {
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

        public Company()
        {
        }

        public Company(string name,
            string address,
            string industry,
            string phoneNo,
            string mobileNo,
            string faxNo,
            string emailAddress,
            string tin,
            string secRegNo,
            string president,
            string corpSec)
        {
            Name = name;
            Address = address;
            Industry = industry;
            PhoneNo = phoneNo;
            MobileNo = mobileNo;
            FaxNo = faxNo;
            EmailAddress = emailAddress;
            TIN = tin;
            SECRegNo = secRegNo;
            President = president;
            CorpSec = corpSec;
        }
        

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Address;
            yield return Industry;
            yield return PhoneNo;
            yield return MobileNo;
            yield return FaxNo;
            yield return EmailAddress;
            yield return TIN;
            yield return SECRegNo;
            yield return President;
            yield return CorpSec;
        }
    }
}

