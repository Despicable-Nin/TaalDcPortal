namespace SeedWork;

public interface IAmCurrentUser
{
    bool IsMarketing { get; }
    bool IsBroker { get; }
    bool IsSales { get; }
    bool IsAdmin { get; }
    
    string Email { get; }
    string IdentityId { get; }
    Task<string> GetToken();
}