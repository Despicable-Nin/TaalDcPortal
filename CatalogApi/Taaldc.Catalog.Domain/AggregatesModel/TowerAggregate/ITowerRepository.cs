using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.TowerAggregate;

public interface ITowerRepository : IRepository<Tower>
{
    Tower Add(Tower project);
    Tower Update(Tower project);
    Task<Tower> GetAsync(int id);
    IEnumerable<Tower> GetListAsync();
}