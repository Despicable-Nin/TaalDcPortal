namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public record BuyerDto
{
    public BuyerDto(string salutation, string firstName, string lastName, string emailAddress, string contactNo,
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

    public string Salutation { get; }

    public string FirstName { get; }


    public string LastName { get; }

    public string EmailAddress { get; }

    public string ContactNo { get; }

    public string Address { get; }
    public string Country { get; }

    public string Province { get; }
    public string TownCity { get; }
    public string ZipCode { get; }
}