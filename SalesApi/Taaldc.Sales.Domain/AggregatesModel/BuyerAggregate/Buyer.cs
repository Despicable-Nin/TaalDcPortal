using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Buyer : Entity, IAggregateRoot
{
    public Buyer(string salutation, string firstName, string lastName, string emailAddress, string contactNo,
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

    protected Buyer()
    {
    }

    public string Salutation { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string EmailAddress { get; private set; }
    public string ContactNo { get; private set; }
    public string Address { get; private set; }
    public string Country { get; private set; }
    public string Province { get; private set; }
    public string TownCity { get; private set; }
    public string ZipCode { get; private set; }


    public void UpdateName(string salutation, string firstName, string lastName)
    {
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateDetails(string emailAddress, string contactNo, string address,
        string country, string province, string townCity, string zipCode)
    {
        Address = address;
        EmailAddress = emailAddress;
        ContactNo = contactNo;
        Country = country;
        Province = province;
        TownCity = townCity;
        ZipCode = zipCode;
    }
}