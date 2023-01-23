using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaalDc.Portal.ViewModels.Users;

public class UpdateUserViewModel
{
    [Required]
    public string Id { get; set; }
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
    public bool IsActive { get; set; }
    
 
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}