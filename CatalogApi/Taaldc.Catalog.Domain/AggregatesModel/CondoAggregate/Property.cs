using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Property : Entity
{
    private readonly List<Tower> _towers;

    public Property(string name, double landArea) : this()
    {
        if (name == default)
            throw new CatalogDomainException(nameof(name), new ArgumentNullException("name is null or empty."));
        
        Name = name;
        LandArea = landArea;
    }

    private Property() => _towers = new List<Tower>();

    public string Name { get; private set; }
    public double LandArea { get; private set; }
    public IReadOnlyCollection<Tower> Towers => _towers.AsReadOnly();
}