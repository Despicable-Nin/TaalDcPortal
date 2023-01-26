using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taaldc.Catalog.Infrastructure
{
    public class CatalogDbContextInitializer
    {
        private readonly ILogger<CatalogDbContextInitializer> _logger;
        private readonly CatalogDbContext _context;
        public CatalogDbContextInitializer(ILogger<CatalogDbContextInitializer> logger, CatalogDbContext context)
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
