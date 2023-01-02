namespace Taaldc.Sales.Domain.Exceptions;

public class SalesDomainException : Exception
{
    public SalesDomainException()
    {
    }

    public SalesDomainException(string? message) : base(message)
    {
    }

    public SalesDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}