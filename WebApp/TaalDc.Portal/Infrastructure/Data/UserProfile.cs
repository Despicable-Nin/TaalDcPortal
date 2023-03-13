using System.ComponentModel.DataAnnotations;

namespace TaalDc.Portal.Data;

public class UserProfile
{
    public UserProfile(string firstName, string lastName, string middleName, string nameSuffix, string identity)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        NameSuffix = nameSuffix;
        Identity = identity;
    }

    public UserProfile(string firstName, string lastName, string middleName, string nameSuffix, string company, string prcLicense, string identity) : this(firstName,lastName,middleName,nameSuffix,identity)
    {
        Company = company;
        PRCLicense = prcLicense;
    }

    [Key] public virtual int UserId { get; protected set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string NameSuffix { get; set; }
    public string Company { get; set; } = "N/A";
    public string PRCLicense { get; set; } = "N/A";

    public string Identity { get; set; }

    public string GetFullname()
    {
        return $"{FirstName} {MiddleName} {LastName} {NameSuffix}"?.Trim();
    }

    public string GetFullname2()
    {
        return $"{LastName}, {FirstName} {NameSuffix}"?.Trim();
    }
}