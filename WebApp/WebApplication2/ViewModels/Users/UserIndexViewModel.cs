namespace WebApplication2.ViewModels.Users;

public class UserIndexViewModel
{
    public UserIndexViewModel()
    {
        _listOfUsers = new List<UserViewModel>();
    }

    private List<UserViewModel> _listOfUsers;
    public ICollection<UserViewModel> ListOfUsers => _listOfUsers.AsReadOnly();

    public void AddUser(UserViewModel model) => _listOfUsers.Add(model);
}

public record UserViewModel
{
    public UserViewModel(string username, string[] roles)
    {
        Username = username;
        Roles = roles;
    }

    public string Username { get; private set; }
    public string[] Roles { get; private set; }

    public string GetRolesAsString() => string.Join(",", Roles ?? Array.Empty<string>());
}