using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;

public interface IPropertyRepository : IRepository<Property>
{
    Property Add(Property project);
    Property Update(Property project);
    Task<Property> GetAsync(int id);
    IEnumerable<Property> GetListAsync();
    Task AddTower(string name, double landArea);
    Task RemoveTower(int propertyId);
}