using Microsoft.EntityFrameworkCore;
using SeedWork;

namespace Taaldc.Marketing.Infrastructure;

public class MarketingDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "Marketing";
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}