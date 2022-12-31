using SeedWork;
using Taaldc.Marketing.Domain.Exceptions;

namespace Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

public class Inquiry : Entity, IAggregateRoot
{

    public string TypeOfInquiry { get; private set; }
    
    private int _statusId;
    public InquiryStatus Status { get; private set; }

    protected Inquiry()
    {
        _statusId = 1;
        AttendedBy = string.Empty;
    }
    public Inquiry(string typeOfInquiry, string message, int propertyId, string property, string remarks, Customer customer) : this()
    {
        TypeOfInquiry = typeOfInquiry;
        Message = message;
        PropertyId = propertyId;
        Property = property;
       
        Remarks = remarks;
        Customer = customer;
    }

   
    public string Message { get; private set; }
    public int PropertyId { get; private set; }
    public string Property { get; private set; }
    public string AttendedBy { get; private set; }
    public string Remarks { get; private set; }
    public Customer Customer { get; private set; }
    
    public void SetStatus(int status) => _statusId = status;
    public void SetType(string inquiryType) =>TypeOfInquiry = inquiryType;
    public void Attend(string email) => AttendedBy = email;

}