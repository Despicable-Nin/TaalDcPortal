using Microsoft.EntityFrameworkCore;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Infrastructure.Repositories;

public class FloorRepository : IFloorRepository
{
    private readonly CatalogDbContext _context;

    public FloorRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public Floor Add(Floor floor) => _context.Floors.Add(floor).Entity;

    public Floor Update(Floor floor) => _context.Floors.Update(floor).Entity;

    public async Task<Floor> GetAsync(int id) =>
        await _context.Floors.Include(i => i.Units).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

    public IEnumerable<Floor> GetListAsync() => _context.Floors.Include(i => i.Units).AsNoTracking().AsEnumerable();
}