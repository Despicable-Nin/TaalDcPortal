using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Property : Entity
{
    public string Name { get; private set; }
    public double LandArea { get; private set; }

    private List<Tower> _towers;
    public IReadOnlyCollection<Tower> Towers => _towers.AsReadOnly();

    private string _projectId;
    public string GetProjectId() => _projectId;

    public Property(string name, double landarea)
    {
        if (name == default)
            throw new CatalogDomainException(nameof(name), new ArgumentNullException("name is null or empty."));
        
        
        Name = name;
        LandArea = landarea;
    }
}