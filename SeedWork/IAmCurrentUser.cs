namespace SeedWork;

public interface IAmCurrentUser
{
    string Name { get; }
    string Email { get; }
    string IdentityId { get; }
    string[] Roles { get; }

    bool IsAdmin();
    bool IsBroker();
}