using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public class AddBuyerCommand : IRequest<int>
{
    public AddBuyerCommand(
        string salutation,
        string firstName,
        string middleName,
        string lastName,
        string emailAddress,
        string phoneNumber,
        string mobileNumber,
        DateTime doB,
        int civilStatusId,
        AddressDto address,
        bool isCorporate,
        CompanyDto company
    )
    {
        Salutation = salutation;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNo = phoneNumber;
        MobileNo = mobileNumber;
        DoB = doB;
        CivilStatusId = civilStatusId;
        HomeAddress = address; 
        Company = company;

    }
    public int? BuyerId { get; private set; }
    public string Salutation { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public string EmailAddress { get; private set; }
    public string MobileNo { get; private set; }
    public string PhoneNo { get; private set; }
    public DateTime DoB { get; private set; }
    public int CivilStatusId { get; private set; }
    public bool IsCorporate { get; private set; }
    //value object
    public CompanyDto Company { get; private set; }
    public AddressDto HomeAddress { get; private set; }
    
}

public class AddressDto
{
    public AddressDto(string street, string city, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
}

public class CompanyDto
{
    public CompanyDto(string name,
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
        Tin = tin;
        SecRegNo = secRegNo;
        President = president;
        CorpSec = corpSec;
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
