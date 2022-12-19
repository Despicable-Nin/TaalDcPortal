namespace Taaldc.Library.Common.Constants;

public class ValidationConstants
{
    public static string NotEmptyErrorMessage(string field)
    {
        return $"{field} is required and must not be empty.";
    }
}