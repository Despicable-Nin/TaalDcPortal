using System.Collections.ObjectModel;
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Buyer : Entity, IAggregateRoot
{
    public Buyer(string salutation,
        string firstName,
        string middleName,
        string lastName,
        DateTime doB,
        int civilStatusId)
    {
        Salutation = salutation;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        DoB = doB;
        CivilStatusId = civilStatusId;
    }

    protected Buyer()
    {
    }

    public string Salutation { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DoB { get; private set; }
    public int CivilStatusId { get; private set; }

    public string EmailAddress { get; private set; }
    public string MobileNo { get; private set; }
    public string PhoneNo { get; private set; }

    public string Occupation { get; private set; }
    public string TIN { get; private set; }
    public string GovIssuedID { get; private set; }
    public DateTime GovIssuedIDValidUntil { get; private set; }

    public int? SpouseId { get; private set; }

    public bool IsCorporate { get; set; }
    public Company? Company { get; set; }
    
    public List<Address> Addresses { get; set; }


    public void UpdateBuyer(string salutation, string firstName, string middleName, string lastName, DateTime doB,
        int civilStatusId)
    {
        Salutation = salutation;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        DoB = doB;
        CivilStatusId = civilStatusId;
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
        TIN = tin;
        GovIssuedID = govIssuedID;
        GovIssuedIDValidUntil = govIssuedIDValidUntil;
    }


}