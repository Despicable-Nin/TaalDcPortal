using Microsoft.EntityFrameworkCore;
using Taaldc.Catalog.Domain.AggregatesModel.TowerAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Infrastructure.Repositories;

public class TowerRepository : ITowerRepository
{
    private readonly CatalogDbContext _context;

    public TowerRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;



    public Tower Add(Tower project) => _context.Towers.Add(project).Entity;

    public Tower Update(Tower project) => _context.Towers.Update(project).Entity;

    public async Task<Tower> GetAsync(int id) =>
        await _context.Towers
            .Include(c => c.Floors)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

    public IEnumerable<Tower> GetListAsync() => _context.Towers
        .Include(c => c.Floors)
        .AsNoTracking()
        .AsEnumerable();


}