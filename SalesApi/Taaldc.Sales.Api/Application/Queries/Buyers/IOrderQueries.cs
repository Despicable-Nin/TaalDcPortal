namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public record BuyerDto
{
    public BuyerDto(string salutation, string firstName, string lastName, string emailAddress, string contactNo, string country, string province, string townCity, string zipCode)
    {
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        ContactNo = contactNo;
        Country = country;
        Province = province;
        TownCity = townCity;
        ZipCode = zipCode;
    }

    public string Salutation { get; private set; }

    public string FirstName { get;private set; }


    public string LastName { get; private set; }

    public string EmailAddress { get; private set; }

    public string ContactNo { get;private set; }

    public string Country { get; private set; }

    public string Province { get;private set; }
    public string TownCity { get; private set; }
    public string ZipCode { get; private set; }
}