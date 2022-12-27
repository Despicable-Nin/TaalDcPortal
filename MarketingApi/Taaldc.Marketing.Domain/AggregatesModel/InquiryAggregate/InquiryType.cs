using SeedWork;

namespace Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

public class InquiryType : Enumeration
{
    public static InquiryType[] Types => new InquiryType[]
    {
        new (1, "SALES INQUIRY"),
        new (2, "LEASING INQUIRY"),
        new (3,"BROKER ACCREDITATION"),
        new (4,"COMMERCIAL SPACE INQUIRY"),
        new (5,"PARKING INQUIRY"),
        new (6,"PROPERTY MANAGEMENT"),
        new(7,"BUSINESS PROPOSAL"),
        new (8, "ADMIN CONCERNS"),
        new (9,"OTHER CONCERNS")
    };
    public InquiryType(int id, string name) : base(id, name)
    {
    }
}