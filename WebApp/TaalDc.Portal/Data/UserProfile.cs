using System.ComponentModel.DataAnnotations;

namespace TaalDc.Portal.Data;

public class UserProfile
{
    [Key]
    public int UserId { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string NameSuffix { get; set; }

    public string GetFullname() => $"{FirstName} {MiddleName} {LastName} {NameSuffix}"?.Trim();
    public string GetFullname2() => $"{LastName}, {FirstName} {NameSuffix}"?.Trim();
    
    public string Identity { get; set; }

}