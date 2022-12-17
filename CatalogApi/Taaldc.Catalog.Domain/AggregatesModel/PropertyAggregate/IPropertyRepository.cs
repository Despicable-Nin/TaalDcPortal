using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;

public interface IPropertyRepository : IRepository<Property>
{
    Property Add(Property property);
    Property Update(Property property);
    Task<Property> GetAsync(int id);
    IEnumerable<Property> GetListAsync();
    
}