namespace TaalDc.Portal.ViewModels.Users;

public class UpdateUserViewModel
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Emailaddress { get; set; }
    public string Role { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}