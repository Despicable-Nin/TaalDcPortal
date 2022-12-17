using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

public interface IFloorRepository : IRepository<Floor>
{
    Floor Add(Floor floor);
    Floor Update(Floor floor);
    Task<Floor> GetAsync(int id);
    IEnumerable<Floor> GetListAsync();
}