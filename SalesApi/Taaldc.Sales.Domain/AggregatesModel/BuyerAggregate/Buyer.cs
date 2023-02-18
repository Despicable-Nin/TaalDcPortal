using System.Collections.ObjectModel;
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Buyer : Entity, IAggregateRoot
{
    #region Ctor
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

        IsCorporate = isCorporate;
        Company = company ?? new Company();

    }
   

    protected Buyer()
    {
        _addresses = new List<Address>();
    }
    #endregion 

    #region Personal Info
    public string Salutation { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DoB { get; private set; }
    
    
    private int _civilStatusId;
    public CivilStatus CivilStatus { get; private set; }
    
    public bool IsCorporate { get; private set; }
    
    //value object
    public Company? Company { get; private set; }
    
    #endregion
    
    #region Contact information

    public string EmailAddress { get; private set; }
    public string MobileNo { get; private set; }
    public string PhoneNo { get; private set; }
    
    #endregion

    #region Misc
    public string Occupation { get; private set; }
    public string Tin { get; private set; }
    public string GovIssuedId { get; private set; }
    public DateTime? GovIssuedIdValidUntil { get; private set; }
    
    #endregion
    
    
    #region Familiar Affiliate
    public int? PartnerId { get; private set; }
    
    #endregion
    
    #region Address

    private List<Address> _addresses;
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    
    #endregion
    
    #region RowVersion
    public byte TimeStamp { get; set; }
    #endregion


    #region Behavior(s)
    public void AddPartnerOrSpouse(int buyerIdOfPartnerOrSpouse)
    {
        PartnerId = buyerIdOfPartnerOrSpouse;
    }

    public void RemovePartnerOrSpouse() => PartnerId = default;

    public void UpdateBuyer(string salutation, string firstName, string middleName, string lastName, DateTime doB,
        int civilStatusId)
    {
        Salutation = salutation;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        DoB = doB;
        _civilStatusId = civilStatusId;
    }

    public void UpdateContactDetails(string emailAddress, string phoneNo, string mobileNo)
    {
        EmailAddress = emailAddress;
        PhoneNo = phoneNo;
        MobileNo = mobileNo;
    }

    public void UpdateMiscInformation(string occupation, string tin, string govIssuedId, DateTime govIssuedIdValidUntil)
    {
        Occupation = occupation;
        Tin = tin;
        GovIssuedId = govIssuedId;
        GovIssuedIdValidUntil = govIssuedIdValidUntil;
    }
    
    public void UpsertAddress(Address newaddress)
    {
        var address = _addresses.SingleOrDefault(i => i.Type == newaddress.Type);

        if (_addresses != default)
        {
            //remove first
            _addresses.Remove(address);
        }

        _addresses.Add(newaddress);
    }

    public Address GetHomeAddress() => _addresses.FirstOrDefault(i => i.Type == AddressTypeEnum.Home);
    public Address GetBillingAddress() => _addresses.FirstOrDefault(i => i.Type == AddressTypeEnum.Billing);
    public Address GetBusinessAddress() => _addresses.FirstOrDefault(i => i.Type == AddressTypeEnum.Business);

    public void UpdateCompany(Company company) => Company = company;

    #endregion

}