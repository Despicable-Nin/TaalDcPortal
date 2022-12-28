using SeedWork;

namespace Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

public class Customer : ValueObject
{
    public Customer(string salutation, string firstName, string lastName, string emailAddress, string contactNo, string country, string province, string townCity)
    {
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        ContactNo = contactNo;
        Country = country;
        Province = province;
        TownCity = townCity;
    }
    
    public Customer(){}

    public string Salutation { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string EmailAddress { get; private set; }
    public string ContactNo { get; private set; }
    public string Country { get; private set; }
    public string Province { get; private set; }
    public string TownCity { get; private set; }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Salutation;
        yield return FirstName;
        yield return LastName;
        yield return EmailAddress;
        yield return ContactNo;
        yield return Country;
        yield return Province;
        yield return TownCity;
    }
}