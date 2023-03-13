using TaalDc.Portal.DTO.Marketing;

namespace TaalDc.Portal.Services;

public interface IMarketingService
{
    Task SubmitInquiry(AddInquiryDto dto);
    Task<InquriesResult> GetInquiries(int pageSize, int pageNumber, int status = 1);
    Task<InquiryDto> GetInquiryById(int id);
    Task UpdateInquiryStatus(UpdateInquiryStatusDto dto);
}