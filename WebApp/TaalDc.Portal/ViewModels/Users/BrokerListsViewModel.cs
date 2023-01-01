namespace TaalDc.Portal.ViewModels.Users;

public record BrokerListsViewModel
{
    public BrokerListsViewModel(string name, string email, string identityId)
    {
        Name = name;
        Email = email;
        IdentityId = identityId;
    }

    private string Name { get; }
    public string Email { get; }
    public string IdentityId { get; }

    public string DisplayName => $"{Name} ({Email})";
}