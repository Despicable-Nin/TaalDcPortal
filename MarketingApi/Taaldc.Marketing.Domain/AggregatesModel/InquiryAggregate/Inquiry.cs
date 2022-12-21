using SeedWork;
using Taaldc.Marketing.Domain.Exceptions;

namespace Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

public class Inquiry : Entity, IAggregateRoot
{
    private int _inquiryTypeId;

    public Inquiry(int inquiryTypeId, string message, int propertyId, string property,  string remarks, Customer customer)
    {
        _inquiryTypeId = inquiryTypeId;
        Message = message;
        PropertyId = propertyId;
        Property = property;
        Remarks = remarks;
        Customer = customer ??
                   throw new MarketingDomainException(nameof(Customer), new Exception("Customer required."));
    }

    public InquiryType InquiryType { get; private set; }
    
    public string Message { get; private set; }
    public int PropertyId { get; private set; }
    public string Property { get; private set; }
    public string AttendedBy { get; private set; }
    public string Remarks { get; private set; }
    
    public Customer Customer { get; private set; }

    private int _statusId;
    public InquiryStatus Status { get; private set; }

    public void SetStatus(int status) => _statusId = status;
    public void SetType(int inquiryTypeId) => _inquiryTypeId = inquiryTypeId;
    public void Attend(string email) => AttendedBy = email;

}