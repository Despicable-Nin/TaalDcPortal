using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Taaldc.Catalog.Infrastructure;

public class CatalogDbContextInitializer
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogDbContextInitializer> _logger;

    public CatalogDbContextInitializer(ILogger<CatalogDbContextInitializer> logger, CatalogDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer()) await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}