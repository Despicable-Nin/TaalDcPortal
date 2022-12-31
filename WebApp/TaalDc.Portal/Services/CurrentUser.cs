using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SeedWork;
using Serilog;

namespace TaalDc.Portal.Services;

public class CurrentUser : IAmCurrentUser
{
    private readonly bool _isAuthenticated;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    
    public CurrentUser(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
    
        Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        IdentityId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid);

        _isAuthenticated = !string.IsNullOrEmpty(Email);
        _userManager = userManager;
        _configuration = configuration;
    }

    public bool IsMarketing { get; }
    public bool IsBroker { get; }
    public bool IsSales { get; }
    public bool IsAdmin { get; }
    public string Email { get; }
    public string IdentityId { get; }
    public async Task<string> GetToken()
    {
        if (_isAuthenticated)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var r in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                }
                
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, ex.InnerException?.Message);
            }

        }

        return string.Empty;
    }
}