namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record BuyerDTO
{
    public BuyerDTO(string salutation, string firstName, string lastName, string emailAddress, string contactNo,
        string address, string country, string province, string townCity, string zipCode)
    {
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        ContactNo = contactNo;
        Address = address;
        Country = country;
        Province = province;
        TownCity = townCity;
        ZipCode = zipCode;
    }

    public string Salutation { get; init; }
    public string FirstName { get; init;}
    public string LastName { get;init; }
    public string EmailAddress { get; init;}
    public string ContactNo { get;init; }
    public string Address { get; init;}
    public string Country { get;init; }
    public string Province { get; init;}
    public string TownCity { get; init;}
    public string ZipCode { get;init; }
}