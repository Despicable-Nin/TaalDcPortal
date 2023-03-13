using System.ComponentModel;

namespace TaalDc.Portal.ViewModels.Users;

public class UserCreateDto
{
    [DisplayName("First Name")] public string FirstName { get; set; }

    [DisplayName("Last Name")] public string LastName { get; set; }

    [DisplayName("Middle Name")] public string MiddleName { get; set; }

    [DisplayName("Name Suffix")] public string NameSuffix { get; set; }

    [DisplayName("Email Address")] public string Emailaddress { get; set; }
    public string Company { get; set; }
    public string PRCLicense { get; set; }

    public string Role { get; set; }
    public string DefaultPassword { get; set; }
}