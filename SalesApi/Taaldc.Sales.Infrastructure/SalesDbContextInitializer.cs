using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taaldc.Sales.Infrastructure
{
    public class SalesDbContextInitializer
    {
        private readonly ILogger<SalesDbContextInitializer> _logger;
        private readonly SalesDbContext _context;
        public SalesDbContextInitializer(ILogger<SalesDbContextInitializer> logger, SalesDbContext context)
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
