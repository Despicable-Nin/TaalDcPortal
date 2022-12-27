using SeedWork;

namespace Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

public class InquiryStatus : Enumeration
{
    public InquiryStatus[] Status => new[]
    {
        new InquiryStatus(1,"New"),
        new InquiryStatus(2,"Open"),
        new InquiryStatus(3,"Closed")
    };
    
    public InquiryStatus(int id, string name) : base(id, name)
    {
    }
}