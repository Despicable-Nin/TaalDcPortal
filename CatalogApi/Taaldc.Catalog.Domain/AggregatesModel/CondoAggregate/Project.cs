using System.Data;
using System.Transactions;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Project : Entity
{
    public string Name { get; private set; }
    public string Developer { get; private set; }
    
    private List<Property> _properties;
    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

    public Project(string name, string developer) : this()
    {
        Name = name;
        Developer = developer;
    }

    private Project()
    {
        _properties = new List<Property>();
    }

    public static Project NewProject()
    {
        var project = new Project();
        return project;
    }

    public void SetName(string name) => Name = name;
    public void SetDeveloper( string developer) => Developer = developer;

    public void AddProperty(string name, double landArea)
    {
        if (_properties.Any(x => x.Name.ToLower() == name.ToLower()))
            throw new CatalogDomainException(nameof(name), new DuplicateNameException($"Duplicate value for '{name}'"));

        _properties.Add(new Property(name, landArea));
    }

    public void RemoveProperty(string id, bool hardDelete = false)
    {
        var remove = _properties.SingleOrDefault(x => x.Id.Equals(id));

        if (remove == default)
            throw new CatalogDomainException(nameof(id), new KeyNotFoundException("{id} is not found."));

        if (hardDelete)
        {
            _properties.Remove(remove);
        }
        else
        {
            _properties.Where(x => x.Id == id).FirstOrDefault().Deactivate();
        }

    }
   
}