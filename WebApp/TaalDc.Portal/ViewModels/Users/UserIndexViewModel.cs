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
    public UserViewModel(string id, string email, string firstName, string lastName, string nameSuffix,
        string middleName, string[] roles, bool isActive, string company, string prc)
    {
        Id = id;
        Email = email;
        FirstName = firstName ?? string.Empty;
        LastName = lastName ?? string.Empty;
        NameSuffix = nameSuffix ?? string.Empty;
        MiddleName = middleName ?? string.Empty;
        Roles = roles;
        IsActive = isActive;
        Company = company;
        PRCLicense = prc;
    }

    public string Id { get; }
    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }
    public string NameSuffix { get; }
    public string MiddleName { get; }
    public string Company { get; set; }
    public string PRCLicense { get; set; }
    public bool IsActive { get; set; }
    public string[] Roles { get; }

    public string GetFullName()
    {
        return
            $"{FirstName} {(!string.IsNullOrEmpty(MiddleName) ? MiddleName[0].ToString() : string.Empty)} {LastName} {NameSuffix}";
    }

    public string? GetRolesAsString()
    {
        if (!Roles.Any()) return null;
        return string.Join(",", Roles ?? Array.Empty<string>());
    }
}