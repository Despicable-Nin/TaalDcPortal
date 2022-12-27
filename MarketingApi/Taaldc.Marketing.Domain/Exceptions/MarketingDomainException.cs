namespace Taaldc.Marketing.Domain.Exceptions;

public class MarketingDomainException : Exception
{
    public MarketingDomainException()
    { }

    public MarketingDomainException(string message)
        : base(message)
    { }

    public MarketingDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}