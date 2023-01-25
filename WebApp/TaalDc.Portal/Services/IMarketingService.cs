using TaalDc.Portal.DTO.Marketing;

namespace TaalDc.Portal.Services;

public interface IMarketingService
{
    Task SubmitInquiry(AddInquiryDto dto);
    Task<InquriesResult> GetInquiries(int pageSize, int pageNumber);
}