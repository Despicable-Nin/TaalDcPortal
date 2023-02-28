namespace Taaldc.Sales.Api.Application.Queries.Buyers;

public class BuyerSQL
{
    public const string SELECT_BUYER_COUNT = " SELECT COUNT(*) AS Count FROM [taaldb_sales].[sales].[buyer] ";

    public const string SELECT_BUYER_FULLNAME_AND_ID =
        @"SELECT [Id] [BuyerId] ,[LastName] + ', ' + [FirstName] +  ' ' + [MiddleName] [Buyer]      
        FROM [taaldb_sales].[sales].[buyer]
        WHERE [IsActive] = 1 ";
    
     public const string SELECT_BUYER_QUERY =
        @"SELECT 
          B.Id [BuyerId]
          ,B.Salutation
          ,B.FirstName
          ,B.MiddleName
          ,B.LastName
          ,B.DoB
          ,C.Name [CivilStatus]
          ,C.Id [CivilStatusId]
          ,B.EmailAddress
          ,b.MobileNo
          ,B.PhoneNo
          ,B.Occupation
          ,B.Tin
          ,B.GovIssuedId
          ,B.GovIssuedIdValidUntil
          ,B.PartnerId
          ,B.IsCorporate
          ,CO.Address [Company_Address]
          ,CO.CorpSec [Company_CorpSec]
          ,CO.TIN [Company_TIN]
          ,CO.EmailAddress [Company_EmailAddress]
          ,CO.FaxNo [Company_FaxNo]
          ,CO.Industry [Company_Industry]
          ,CO.MobileNo [Company_MobileNo]
          ,CO.Name [Company_Name]
          ,CO.PhoneNo [Company_PhoneNo]
          ,CO.President [Company_President]
          ,CO.SECRegNo [Company_SECRegNo]
          ,HA.Street [HomeAddress_Street]
          ,HA.City [HomeAddress_City]
          ,HA.[State] [HomeAddress_State]
          ,HA.[Country] [HomeAddress_Country]
          ,HA.ZipCode [HomeAddress_ZipCode]
          ,BuA.Street [BusinessAddress_Street]
          ,BuA.City [BusinessAddress_City]
          ,BuA.[State] [BusinessAddress_State]
          ,BuA.[Country] [BusinessAddress_Country]
          ,BuA.ZipCode [BusinessAddress_ZipCode]
          ,BA.Street [BillingAddress_Street]
          ,BA.City [BillingAddress_City]
          ,BA.[State] [BillingAddress_State]
          ,BA.[Country] [BillingAddress_Country]
          ,BA.ZipCode [BillingAddress_ZipCode]
        FROM [taaldb_sales].[sales].[buyer] B
          LEFT JOIN taaldb_sales.sales.civilStatus C ON C.Id = B.CivilStatusId
          LEFT JOIN taaldb_sales.sales.company CO ON CO.BuyerId = B.Id
          LEFT JOIN taaldb_sales.sales.address HA ON HA.BuyerId = B.Id AND HA.[Type] = 1
          LEFT JOIN taaldb_sales.sales.address BuA ON BuA.BuyerId = B.Id AND BuA.[Type] = 2
          LEFT JOIN taaldb_sales.sales.address BA ON BA.BuyerId = B.Id AND BA.[Type] = 3";

  
    
}