using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taaldc.Marketing.Infrastructure
{
    public class MarketingDbContextInitializer
    {
        private readonly ILogger<MarketingDbContextInitializer> _logger;
        private readonly MarketingDbContext _context;
        public MarketingDbContextInitializer(ILogger<MarketingDbContextInitializer> logger, MarketingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    var pendingMigration = await _context.Database.GetPendingMigrationsAsync();

                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
    }
}
