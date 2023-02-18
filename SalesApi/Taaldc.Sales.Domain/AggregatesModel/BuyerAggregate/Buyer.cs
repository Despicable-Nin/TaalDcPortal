using System.Collections.ObjectModel;
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Buyer : Entity, IAggregateRoot
{
    public Buyer(string salutation,
        string firstName,
        string middleName,
        string lastName,
        string emailAddress,
        string phoneNumber,
        string mobileNumber,
        DateTime doB,
        int civilStatusId,
        Address address,
        bool isCorporate,
        Company? company
        ) : this()
    {
        Salutation = salutation;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNo = phoneNumber;
        MobileNo = mobileNumber;
        DoB = doB;
        _civilStatusId = civilStatusId;
        _addresses.Add(address);
        Company = company;

    }
   

    protected Buyer()
    {
        _addresses = new List<Address>();
    }

    public string Salutation { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DoB { get; private set; }
    
    private int _civilStatusId;
    public CivilStatus CivilStatus { get; private set; }

    public string EmailAddress { get; private set; }
    public string MobileNo { get; private set; }
    public string PhoneNo { get; private set; }

    public string Occupation { get; private set; }
    public string Tin { get; private set; }
    public string GovIssuedId { get; private set; }
    public DateTime? GovIssuedIdValidUntil { get; private set; }

    public int? SpouseId { get; private set; }

    public bool IsCorporate { get; private set; }

    //private int? _companyId;
    public Company? Company { get; private set; }

    private List<Address> _addresses;
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();


    public void UpdateBuyer(string salutation, string firstName, string middleName, string lastName, DateTime doB,
        int civilStatusId)
    {
        Salutation = salutation;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        DoB = doB;
        //CivilStatusId = civilStatusId;
    }

    public void UpdateContactDetails(string emailAddress, string phoneNo, string mobileNo)
    {
        EmailAddress = emailAddress;
        PhoneNo = phoneNo;
        MobileNo = mobileNo;
    }

    public void UpdateIDInformation(string occupation, string tin, string govIssuedID, DateTime govIssuedIDValidUntil)
    {
        Occupation = occupation;
        Tin = tin;
        GovIssuedId = govIssuedID;
        GovIssuedIdValidUntil = govIssuedIDValidUntil;
    }


}