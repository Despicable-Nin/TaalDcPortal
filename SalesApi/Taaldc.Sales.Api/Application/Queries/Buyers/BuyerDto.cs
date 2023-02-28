using Microsoft.Build.ObjectModelRemoting;

namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public record BuyerResultDto : IResultDto
{
    public int BuyerId { get; init; }
    public string Salutation { get; init; }
    public string FirstName { get; init; }
    public string MiddleName { get; init; }
    public string LastName { get; init; }
    public DateTime Dob { get; init; }  
    public string CivilStatus { get; init; }
    public int CivilStatusId { get; init; }
    public string EmailAddress { get; init; }
    public string MobileNo { get; init; }
    public string? PhoneNo { get; init; }
    public string? Occupation { get; init; }
    public string? Tin { get; init; }
    public string? GovIssuedId { get; init; }
    public DateTime? GovIssuedIdValidUntil { get; init; }
    public int? PartnerId { get; init; }
    public bool IsCorporate { get; init; }
    public string? Company_Address { get; init; }
    public string? Company_TIN { get; init; }
    public string? Company_CorpSec { get; init; }
    public string? Company_EmailAddress { get; init; }
    public string? Company_FaxNo { get; init; }
    public string? Company_Industry { get; init; }
    public string? Company_MobileNo { get; init; }
    public string? Company_Name { get; init; }
    public string? Company_PhoneNo { get; init; }
    public string? Company_President { get; init; }
    public string? Company_SECRegNo { get; init; }

    public string? HomeAddress_Street { get; init; }
    public string? HomeAddress_City { get; init; }
    public string? HomeAddress_State { get; init; }
    public string? HomeAddress_Country { get; init; }
    public string? HomeAddress_ZipCode { get; init; }

    public string? BusinessAddress_Street { get; init; }
    public string? BusinessAddress_City { get; init; }
    public string? BusinessAddress_State { get; init; }
    public string? BusinessAddress_Country { get; init; }
    public string? BusinessAddress_ZipCode { get; init; }


    public string? BillingAddress_Street { get; init; }
    public string? BillingAddress_City { get; init; }
    public string? BillingAddress_State { get; init; }
    public string? BillingAddress_Country { get; init; }
    public string? BillingAddress_ZipCode { get; init; }

}

public record BuyerDropdownResultDto : IResultDto
{
    public int BuyerId { get; init; }
    public string FullName { get; init; }
}