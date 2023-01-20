using System.ComponentModel.DataAnnotations;

namespace TaalDc.Portal.Data;

public class UserProfile
{
    [Key]
    public int UserId { get; set; }
    
    public string Name { get; set; }

    public string Identity { get; set; }

}