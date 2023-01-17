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

    public OrderQueries(string connectionString, SalesDbContext context)
    {
        _connectionString = connectionString ??
                            throw new SalesDomainException(nameof(OrderQueries),
                                new ArgumentNullException($"{connectionString} cannot be null."));

        _context = context;
    }

    public async Task<IEnumerable<PreSellingDTO>> GetPresellingReport()
    {
        var query = $"SELECT O.Id " +
                    $",O.[Code] " +
                    $",O.[Broker] " +
                    $",O.[Remarks] " +
                    $",O.[FinalPrice] " +
                    $",O.[IsRefundable] " +
                    $",O.[StatusId] " +
                    $",O.[BuyerId] " +
                    $",B.[Salutation] " +
                    $",B.[FirstName] " +
                    $",B.[LastName] " +
                    $",B.[EmailAddress] " +
                    $",B.[ContactNo] " +
                    $",B.[Country] " +
                    $",B.[Province] " +
                    $",B.[TownCity] " +
                    $",B.[ZipCode] " +
                    $",B.[CreatedBy] " +
                    $",O.[BuyerId] " +
                    $",U.[PropertyId] " +
                    $",U.[TowerId] " +
                    $",U.[FloorId] " +
                    $",U.[UnitId] " +
                    $",U.[ScenicViewId] " +
                    $",U.[UnitTypeId] " +
                    $",U.[Property] " +
                    $",U.[Tower] " +
                    $",U.[Floor] " +
                    $",U.[Unit] " +
                    $",U.[ScenicView] " +
                    $",U.[UnitType] " +
                    $",U.[UnitArea] " +
                    $",U.[BalconyArea] " +
                    $",U.[UnitStatus] " +
                    $",U.[UnitStatusId] " +
                    $",U.[OriginalPrice] " +
                    $",U.[SellingPrice] " +
                    $"FROM [taaldb_sales].[sales].[order] O  " +
                    $"JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id " +
                    $"JOIN [taaldb_sales].[sales].[unitreplica] U ON O.UnitId = U.UnitId " +
                    $"ORDER BY O.[BuyerId]";


        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<PreSellingDTO>(query);

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

        if (tran == default) throw new SalesDomainException(nameof(GetPayments), new Exception("Order not found"));

        return tran.Payments.Select(i =>
            new PaymentDTO(i.Id, i.ActualPaymentDate, i.ConfirmationNumber,
                i.GetPaymentTypeId(), i.PaymentType?.Name, i.GetTransactionTypeId(), i.VerifiedBy,
                i.GetPaymentStatusId(),
                i.Status?.Name, i.PaymentMethod, i.AmountPaid, i.Remarks, tran.Id, i.CorrelationId.ToString(),
                i.TransactionType?.Name));

    }
}