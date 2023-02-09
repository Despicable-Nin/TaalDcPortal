using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.Infrastructure.Repositories;

public class UnitTypeRepository : IUnitTypeRepository
{
    private readonly CatalogDbContext _context;

    public UnitTypeRepository(CatalogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public UnitType Add(UnitType unitType)
    {
        return _context.UnitTypes.Add(unitType).Entity;
    }

    public async Task<UnitType> GetAsync(int id)
    {
        return await _context.UnitTypes.FirstOrDefaultAsync(i => i.Id == id);
    }

    public UnitType Update(UnitType unitType)
    {
        return _context.UnitTypes.Update(unitType).Entity;
    }
}