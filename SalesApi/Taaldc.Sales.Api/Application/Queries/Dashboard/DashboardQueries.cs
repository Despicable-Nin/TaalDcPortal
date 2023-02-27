using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public partial class DashboardQueries : IDashboardQueries
{

    private readonly string _connectionString;
    private readonly SalesDbContext _context;


    public DashboardQueries(string connectionString, SalesDbContext context)
    {
        _connectionString = connectionString ??
                            throw new SalesDomainException(nameof(DashboardQueries),
                                new ArgumentNullException($"{connectionString} cannot be null."));

        _context = context;
    }

    public async Task<int> GetAvailableUnitCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i =>
                new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.AVAILABLE);
    }

    public async Task<int> GetReservedUnitCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.RESERVED);
    }

    public async Task<int> GetSoldUnitCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.SOLD);
    }

    public async Task<int> GetBlockedUnitCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);
    }

    public async Task<int> GetAvailableParkingCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 6, 7 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);
    }

    public async Task<int> GetReservedParkingCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 6, 7 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);
    }

    public async Task<int> GetSoldParkingCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 6, 7 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);
    }

    public async Task<int> GetBlockedParkingCount()
    {
        return await _context.Units
            .AsNoTracking()
            .CountAsync(i => new[] { 6, 7 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);
    }

    public async Task<IEnumerable<UnitAvailabilitySummaryByStatusDTO>> GetCountByUnitStatus(int[] unitTypes)
    {
        var unitTypesJoined = string.Join(",", unitTypes);

        var query = "SELECT " +
                    "UnitStatus, " +
                    "COUNT(*) AS [Count] " +
                    "FROM sales.unitreplica " +
                    $"WHERE UnitTypeId IN ({unitTypesJoined}) " +
                    "GROUP BY UnitStatus";

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<UnitAvailabilitySummaryByStatusDTO>(query);

        return result;
    }

    internal enum UnitStatus
    {
        AVAILABLE = 1,
        SOLD = 2,
        RESERVED = 3,
        BLOCKED = 4
    }
}