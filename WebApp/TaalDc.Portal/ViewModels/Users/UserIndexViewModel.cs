namespace TaalDc.Portal.ViewModels.Users;

public class UserIndexViewModel
{
    private readonly List<UserViewModel> _listOfUsers;

    public UserIndexViewModel()
    {
        _listOfUsers = new List<UserViewModel>();
    }

    public ICollection<UserViewModel> ListOfUsers => _listOfUsers.AsReadOnly();

    public void AddUser(UserViewModel model)
    {
        _listOfUsers.Add(model);
    }
}

public record UserViewModel
{
    public UserViewModel(string username, string email, string id, string[] roles)
    {
        Username = username;
        Email = email;
        Id = id;
        Roles = roles;
    }

    public string Id { get; }
    public string Email { get; }
    public string Username { get; }
    public string[] Roles { get; }

    public string? GetRolesAsString()
    {
        if (!Roles.Any()) return null;
        return string.Join(",", Roles ?? Array.Empty<string>());
    }
}