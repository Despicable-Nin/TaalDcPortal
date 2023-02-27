namespace Taaldc.Sales.Api.Application.Queries.Orders;

internal static class OrderSQL
{
    public const string SELECT_ORDER_DETAILS = @"SELECT
    O.Id AS OrderId
    ,[Code]
    ,[Broker]
    ,[ReservationExpiresOn]
    ,[BuyerId]
    ,O.[StatusId]
    ,S.Name [Status]
    ,[Price]
    ,[UnitId]
    ,O.Discount [Discount]    
    ,(SELECT TOP 1 ActualPaymentDate FROM Payment WHERE OrderId = O.Id ORDER BY ActualPaymentDate ASC) [TransactionDate]
    ,ConfirmationNumber
    ,ActualPaymentDate
    ,P.StatusId [PaymentStatusId]
    ,PS.Name [PaymentStatus]
    ,PaymentMethod
    ,PaymentTypeId
    ,PT.Name [PaymentType]
    ,TransactionTypeId
    ,T.Name [TransactionType]
    ,VerifiedBy
    ,AmountPaid
        FROM [taaldb_sales].[sales].[order] O
        LEFT JOIN [taaldb_sales].[sales].[orderitem] I ON I.OrderId = O.Id
        LEFT JOIN [taaldb_sales].[sales].[orderstatus] S ON O.StatusId = S.Id
        LEFT JOIN [taaldb_sales].[dbo].payment P ON P.OrderId = O.Id 
        LEFT JOIN [taaldb_sales].[dbo].paymentstatus PS ON PS.Id = P.StatusId
        LEFT JOIN [taaldb_sales].[dbo].paymenttype PT ON PT.Id = P.PaymentTypeId
        LEFT JOIN [taaldb_sales].[dbo].transactiontype T ON T.Id = P.TransactionTypeId ";
    
    public static string SELECT_PAYMENTS(int id) => @$"SELECT P.Id,
                                          P.ActualPaymentDate, 
                                          P.ConfirmationNumber,
                                          P.PaymentTypeId, 
                                          PT.[Name] AS PaymentType, 
                                          P.TransactionTypeId, 
                                          T.[Name] AS TransactionType, 
                                          P.VerifiedBy, 
                                          P.StatusId AS PaymentStatusId, 
                                          PS.[Name] AS PaymentStatus, 
                                          P.PaymentMethod, 
                                          P.AmountPaid, 
                                          ISNULL(P.Remarks, ''), 
                                          P.CorrelationId, 
                                          P.OrderId FROM dbo.payment P 
                                          JOIN dbo.paymenttype PT ON
                                          P.PaymentTypeId = PT.Id JOIN
                                          dbo.transactiontype T ON
                                          P.TransactionTypeId = T.Id JOIN
                                          dbo.paymentstatus PS ON
                                          P.StatusId = PS.Id
                                          WHERE OrderId = '{id}' ";
    
    public const string WITH_PAYMENTCTE =
        "WITH PaymentCTE AS ( SELECT P.*, ROW_NUMBER() OVER (PARTITION BY P.OrderId ORDER BY P.id) as RowNum FROM [taaldb_sales].[dbo].[payment] P) ";

    public const string SELECT_UNITS_WITH_BUYER = @$"{WITH_PAYMENTCTE} SELECT  O.Id
        ,O.[Code] 
        ,O.[Broker] 
        ,O.[Remarks] 
        ,O.[StatusId]
        ,OS.[Name] AS Status
        ,O.[BuyerId] 
        ,B.[Salutation]
        ,B.[FirstName] 
        ,B.[LastName]
        ,B.[EmailAddress] 
        ,B.[PhoneNo] 
        ,B.[MobileNo] 
        ,A.[Street]
        ,A.[Country] 
        ,A.[State] 
        ,A.[City] 
        ,A.[ZipCode] 
        ,O.[Id] [OrderId] 
        ,B.[CreatedBy] 
        ,O.[BuyerId] 
        ,U.[PropertyId] 
        ,U.[TowerId] 
        ,U.[FloorId] 
        ,U.[UnitId] 
        ,U.[ScenicViewId]
        ,U.[UnitTypeId] 
        ,U.[Property] 
        ,U.[Tower] 
        ,U.[Floor] 
        ,U.[Unit] 
        ,U.[ScenicView] 
        ,U.[UnitType] 
        ,U.[UnitArea] 
        ,U.[BalconyArea] 
        ,U.[UnitStatus]
        ,U.[UnitStatusId]
        ,U.[OriginalPrice] 
        ,U.[SellingPrice]
        ,P.[ActualPaymentDate] AS TransactionDate 
    FROM [taaldb_sales].[sales].[unitreplica] U 
        LEFT JOIN [taaldb_sales].[sales].[orderitem] OI ON OI.UnitId = U.UnitId 
        LEFT Join [taaldb_sales].[sales].[order] O ON O.Id = OI.OrderId
        LEFT JOIN [taaldb_sales].[sales].[orderstatus] OS ON O.[StatusId] = OS.Id  
        LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id 
        LEFT JOIN [taaldb_sales].[sales].[address] A ON A.BuyerId = B.Id AND A.[Type] = 1
        LEFT JOIN  PaymentCTE P ON P.OrderId = O.Id AND P.RowNum = 1";
   


}