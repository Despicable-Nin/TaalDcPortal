using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public interface IProjectRepository : IRepository<Project>
{
    Project Add(Project project);
    Project Update(Project project);
    Task<Project> GetAsync(int id);
    IEnumerable<Project> GetListAsync();
    Task<int> AddProperty(int projectId, string name, double landArea);
    Task RemoveProperty(int projectId, int propertyId);
}