using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public record AddBuyerCommand : IRequest<int>
{
    public AddBuyerCommand(
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
        CompanyDto company
    )
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
    
    public AddBuyerCommand(){}
    public string Salutation { get;init;}
    public string FirstName { get; init;}
    public string MiddleName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }
    public string MobileNo { get;init;}
    public string PhoneNo { get; init;}
    public DateTime DoB { get;init; }
    public int CivilStatusId { get; init; }
    public bool IsCorporate { get;init; }
    //value object
    public CompanyDto Company { get; init; }
    public AddressDto HomeAddress { get; init;}
    
}

public record AddressDto
{
    public string Street { get; init;}
    public string City { get; init;}
    public string State { get; init;}
    public string Country { get; init; }
    public string ZipCode { get; init; }
}

public record CompanyDto
{
    public string Name { get; init;}
    public string Address { get; init; }
    public string Industry { get; init; }
    public string PhoneNo { get; init; }
    public string MobileNo { get; init; }
    public string FaxNo { get; init; }
    public string EmailAddress { get; init; }
    public string Tin { get; init; }
    public string SecRegNo { get; init; }
    public string President { get; init; }
    public string CorpSec { get; init; }
}
