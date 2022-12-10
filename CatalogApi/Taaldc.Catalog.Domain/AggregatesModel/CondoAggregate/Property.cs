using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Property : Entity
{
    private string _projectId;

    private List<Tower> _towers;

    public Property(string name, double landarea)
    {
        if (name == default)
            throw new CatalogDomainException(nameof(name), new ArgumentNullException("name is null or empty."));


        Name = name;
        LandArea = landarea;
    }

    public string Name { get; }
    public double LandArea { get; }
    public IReadOnlyCollection<Tower> Towers => _towers.AsReadOnly();

    public string GetProjectId()
    {
        return _projectId;
    }
}