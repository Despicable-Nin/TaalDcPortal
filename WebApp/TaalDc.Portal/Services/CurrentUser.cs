using SeedWork;

namespace TaalDc.Portal.Services;

public class CurrentUser : IAmCurrentUser
{
    public bool IsMarketing { get; }
    public bool IsBroker { get; }
    public bool IsSales { get; }
    public bool IsAdmin { get; }
    public string Email { get; }
    public string IdentityId { get; }
    public string GetToken()
    {
        throw new NotImplementedException();
    }
}