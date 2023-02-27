using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace TaalDc.Portal.Infrastructure;

public class HttpClientAuthorizationDelegatingHandler
    : DelegatingHandler
{
    private readonly IConfiguration _configuration;
    private readonly string _email;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly bool _isAuthenticated;
    private readonly ILogger<HttpClientAuthorizationDelegatingHandler> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public HttpClientAuthorizationDelegatingHandler(
        ILogger<HttpClientAuthorizationDelegatingHandler> logger,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;

        _email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        _isAuthenticated = !string.IsNullOrEmpty(_email);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authorizationHeader = _httpContextAccessor.HttpContext
            .Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
            request.Headers.Add("Authorization", new List<string> { authorizationHeader });

        var token = await GetToken();

        if (token != null) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        _logger.LogDebug("Logging HTTPClient request...", request);
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetToken()
    {
        if (_isAuthenticated)
            try
            {
                var user = await _userManager.FindByEmailAsync(_email);
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Email, user.NormalizedEmail),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.NormalizedUserName)
                };

                foreach (var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                // var tokenDescriptor = new SecurityTokenDescriptor()
                // {
                //     Subject = new ClaimsIdentity(claims),
                //     Expires = DateTime.Now.AddHours(8),
                //     Issuer = _configuration["JWT:ValidIssuer"],
                //     Audience = _configuration["JWT:ValidAudience"],
                //     SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                // };
                //
                // var tokenHandler = new JwtSecurityTokenHandler();
                // var token = tokenHandler.CreateToken(tokenDescriptor);
                // var stringToken = tokenHandler.WriteToken(token);
                //return stringToken;
                var token = new JwtSecurityToken(
                    _configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(8),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                //TODO: we can try storing this to Session or Cookie after..
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, ex.InnerException?.Message);
            }

        return string.Empty;
    }
}