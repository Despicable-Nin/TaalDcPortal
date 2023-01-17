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

public class CreateUserViewModel
{
    public string Username { get; set; }
    public string Emailaddress { get; set; }
    public string Role { get; set; }
    public string DefaultPassword { get; set; }
}

public record UserViewModel
{
    public UserViewModel(string username, string[] roles)
    {
        Username = username;
        Roles = roles;
    }

    public string Username { get; }
    public string[] Roles { get; }

    public string? GetRolesAsString()
    {
        if (!Roles.Any()) return null;
        return string.Join(",", Roles ?? Array.Empty<string>());
    }
}