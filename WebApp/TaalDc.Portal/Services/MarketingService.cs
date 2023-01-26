using System.Drawing.Printing;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TaalDc.Portal.DTO.Marketing;
using TaalDc.Portal.Infrastructure;

namespace TaalDc.Portal.Services;

public class MarketingService : IMarketingService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MarketingService> _logger;

    private readonly string _removeServiceBaseUrl;
    private readonly IOptions<AppSettings> _settings;


    public MarketingService(IOptions<AppSettings> settings, HttpClient httpClient, ILogger<MarketingService> logger)
    {
        _settings = settings;
        _httpClient = httpClient;
        _logger = logger;

        _removeServiceBaseUrl = $"{settings.Value.MarketingUrl}";
    }

    public Task SubmitInquiry(AddInquiryDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<InquriesResult> GetInquiries(int pageSize, int pageNumber)
    {
        var uri = API.Marketing.GetInquiries(_removeServiceBaseUrl, pageSize, pageNumber);
        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<InquriesResult>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<InquiryDto> GetInquiryById(int id)
    {
        var uri = API.Marketing.GetInquiry(_removeServiceBaseUrl, id);
        var responseString = await _httpClient.GetStringAsync(uri);

        var result = JsonSerializer.Deserialize<InquiryDto>(responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }
}