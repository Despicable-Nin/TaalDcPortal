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
    
    public Floor Add(Floor floor)
    {
        throw new NotImplementedException();
    }

    public Floor Update(Floor floor)
    {
        throw new NotImplementedException();
    }

    public Task<Floor> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Floor> GetListAsync()
    {
        throw new NotImplementedException();
    }
}