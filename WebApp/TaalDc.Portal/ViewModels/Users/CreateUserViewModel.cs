using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaalDc.Portal.ViewModels.Users;

public class CreateUserViewModel
{
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    [DisplayName("Last Name")]
    public string LastName { get; set; }
    [DisplayName("Middle Name")]
    public string MiddleName { get; set; }
    [DisplayName("Name Suffix")]
    public string NameSuffix { get; set; }
    [DisplayName("Email Address")]
    public string Emailaddress { get; set; }
    public string Role { get; set; }
    public string DefaultPassword { get; set; }
}