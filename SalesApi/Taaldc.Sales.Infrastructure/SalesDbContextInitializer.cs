using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Taaldc.Sales.Infrastructure;

public class SalesDbContextInitializer
{
    private readonly SalesDbContext _context;
    private readonly ILogger<SalesDbContextInitializer> _logger;

    public SalesDbContextInitializer(ILogger<SalesDbContextInitializer> logger, SalesDbContext context)
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