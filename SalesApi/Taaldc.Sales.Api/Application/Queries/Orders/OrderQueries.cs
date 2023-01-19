using System.Diagnostics;
using Dapper;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public class OrderQueries : IOrderQueries
{
    private readonly string _connectionString;
    private readonly SalesDbContext _context;

    private const string UnitQuery =
        $" SELECT " + 
        $" O.Id ,O.[Code] ,O.[Broker] ,O.[Remarks] ,O.[FinalPrice] ,O.[IsRefundable] ,O.[StatusId] ,O.[BuyerId] ,B.[Salutation] ,B.[FirstName] ,B.[LastName] ,B.[EmailAddress] ,B.[ContactNo] ,B.[Country] ,B.[Province] ,B.[TownCity] ,B.[ZipCode] ,O.[Id] [OrderId] ,B.[CreatedBy] ,O.[BuyerId] ,U.[PropertyId] ,U.[TowerId] ,U.[FloorId] ,U.[UnitId] ,U.[ScenicViewId] ,U.[UnitTypeId] ,U.[Property] ,U.[Tower] ,U.[Floor] ,U.[Unit] ,U.[ScenicView] ,U.[UnitType] ,U.[UnitArea] ,U.[BalconyArea] ,U.[UnitStatus],U.[UnitStatusId],U.[OriginalPrice] ,U.[SellingPrice] " + 
        $" FROM [taaldb_sales].[sales].[unitreplica] U  LEFT JOIN [taaldb_sales].[sales].[order] O ON O.UnitId = U.UnitId LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id ";

    public OrderQueries(string connectionString, SalesDbContext context)
    {
        _connectionString = connectionString ??
                            throw new SalesDomainException(nameof(OrderQueries),
                                new ArgumentNullException($"{connectionString} cannot be null."));

        _context = context;
    }

    public async Task<IEnumerable<Unit_Order_DTO>> GetUnitAndOrdersByAvailability(
        int unitStatusId, 
        int pageNumber, 
        int pageSize,   
        int? floorId,
        int? unitTypeId,
        int? viewId)
    {
       var query =
           $"{UnitQuery} WHERE U.UnitStatusId = ISNULL({(unitStatusId > 0 ? $"'{unitStatusId}'" : "NULL")}, U.UnitStatusId) AND U.FloorId = ISNULL({(floorId > 0 ? $"'{floorId}'" : "NULL")}, U.FloorId) AND U.UnitTypeId = ISNULL({(unitTypeId > 0 ? $"'{unitTypeId}'" : "NULL")}, U.UnitTypeId) AND U.ScenicViewId = ISNULL({(viewId > 0 ? $"'{viewId}'" : "NULL")},U.ScenicViewId) ORDER BY U.[UnitId] OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
       
        await using var connection = new SqlConnection(_connectionString);
      
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<Unit_Order_DTO>(query);

        return result;

    }

    public async Task<IEnumerable<PaymentDTO>> GetPayments(int id)
    {
        var tran = await _context.Orders
            .Include(i => i.Payments)
            .ThenInclude(i => i.PaymentType)
            .Include(i => i.Payments)
            .ThenInclude(i => i.Status)
            .Include(i => i.Payments)
            .ThenInclude(i => i.TransactionType)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        
        if (tran is null) throw new SalesDomainException(nameof(GetPayments), new Exception("Order not found"));

        var result = tran.Payments.Select(i =>
            new PaymentDTO(
                i.Id, 
                i.ActualPaymentDate, 
                i.ConfirmationNumber,
                i.GetPaymentTypeId(),
                i.PaymentType?.Name,
                i.GetTransactionTypeId(), 
                i.VerifiedBy,
                i.GetPaymentStatusId(),
                i.Status?.Name,
                i.PaymentMethod,
                i.AmountPaid,
                i.Remarks,
                tran.Id,
                i.CorrelationId.ToString(),
                i.TransactionType?.Name));
        
        return result;

    }
}