using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using SeedWork;

namespace Taaldc.Sales.Infrastructure;

public class SalesDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "sales";
    
    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return null;
    }
}