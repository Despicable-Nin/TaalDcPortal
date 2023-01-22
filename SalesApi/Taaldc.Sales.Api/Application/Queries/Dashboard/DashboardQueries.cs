using Microsoft.EntityFrameworkCore;
using Taaldc.Sales.Domain.Exceptions;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public partial class DashboardQueries : IDashboardQueries
{
    internal enum UnitStatus : int
    {
        AVAILABLE = 1,
        SOLD = 2,
        RESERVED = 3,
        BLOCKED = 4
    }
    private readonly string _connectionString;
    private readonly SalesDbContext _context;
    private const string COUNT_UNIT_QUERY = "SELECT COUNT(*) [Count] " +
                                            "FROM [taaldb_sales].[sales].[unitreplica] U " +
                                            "LEFT JOIN [taaldb_sales].[sales].[order] O ON O.UnitId = U.UnitId " +
                                            "LEFT JOIN [taaldb_sales].[sales].[buyer] B ON O.BuyerId = B.Id";
    
    

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
            .CountAsync(i => new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.AVAILABLE);
       
    }

    public async Task<int> GetReservedUnitCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i =>  new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId)  && i.UnitStatusId == (int)UnitStatus.RESERVED);

    public async Task<int> GetSoldUnitCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i => new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId)  && i.UnitStatusId == (int)UnitStatus.SOLD);

    public async Task<int> GetBlockedUnitCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i =>  new[] { 2, 3, 4, 5, 8 }.Contains(i.UnitTypeId)  && i.UnitStatusId == (int)UnitStatus.BLOCKED);

    public async Task<int> GetAvailableParkingCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i => new[] { 6,7 }.Contains(i.UnitTypeId)  && i.UnitStatusId == (int)UnitStatus.BLOCKED);

    public async Task<int> GetReservedParkingCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i =>new[] { 6,7 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);

    public async Task<int> GetSoldParkingCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i => new[] { 6,7 }.Contains(i.UnitTypeId) && i.UnitStatusId == (int)UnitStatus.BLOCKED);

    public async Task<int> GetBlockedParkingCount() =>
        _context.Units
            .AsNoTracking()
            .Count(i =>new[] { 6,7 }.Contains(i.UnitTypeId)&& i.UnitStatusId == (int)UnitStatus.BLOCKED);
}