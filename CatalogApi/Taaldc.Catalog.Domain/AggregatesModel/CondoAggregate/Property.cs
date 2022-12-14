using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Property : Entity
{
    private string _projectId;

   

    public Property(string name, double landArea)
    {
        if (name == default)
            throw new CatalogDomainException(nameof(name), new ArgumentNullException("name is null or empty."));


        Name = name;
        LandArea = landArea;
    }

    public string Name { get; private set; }
    public double LandArea { get; private set; }
    //
    // private List<Tower> _towers;
    // public IReadOnlyCollection<Tower> Towers => _towers.AsReadOnly();

    public string GetProjectId()
    {
        return _projectId;
    }
}