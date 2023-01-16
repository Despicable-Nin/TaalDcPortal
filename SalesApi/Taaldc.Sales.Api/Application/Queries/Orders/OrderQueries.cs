using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Api.Application.Queries.Orders;

public class OrderQueries : IOrderQueries
{
    private readonly string _connectionString;

    public OrderQueries(string connectionString)
    {
        _connectionString = connectionString ??
                            throw new SalesDomainException(nameof(OrderQueries),
                                new ArgumentNullException($"{connectionString} cannot be null."));
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
                    $"JOIN [taaldb_sales].[sales].[unitreplica] U ON O.UnitId = U.UnitId";


        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);
        
        
    }


}