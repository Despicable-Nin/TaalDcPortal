using Microsoft.EntityFrameworkCore;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly CatalogDbContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public PropertyRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public Property Add(Property property) => _context.Add(property).Entity;

    public Property Update(Property project) => _context.Update(project).Entity;

    public async Task<Property> GetAsync(int id) =>
        await _context.Properties.Include(i => i.Towers).FirstOrDefaultAsync(i => i.Id == id);

    public IEnumerable<Property> GetListAsync() => _context.Properties.Include(i=> i.Towers).AsEnumerable();
    
  
}