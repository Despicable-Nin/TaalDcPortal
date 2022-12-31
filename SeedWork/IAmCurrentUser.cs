namespace SeedWork;

public interface IAmCurrentUser
{
    string Email { get; }
    string IdentityId { get; }
    string[] Roles { get; }
    Task<string> GetToken();
}