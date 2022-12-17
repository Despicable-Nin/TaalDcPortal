using Microsoft.VisualBasic.CompilerServices;
using Shouldly;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

namespace Catalog.Domain.Tests;

public class UnitTest1
{
    private const string name = nameof(name);
    private const string developer = nameof(developer);
    
    [Fact]
    public void Can_create_instance_by_factory_successful()
    {
        var project = Project.NewProject();

        project.SetName("just another name");
        project.Developer = "taal dc";
        
        project.ShouldNotBeNull();
        project.Name.ShouldNotBeNull();
        project.Name.ShouldBe("just another name");
        project.Developer.ShouldNotBeNull();
        project.Developer.ShouldBe("taal dc");
    }

    [Fact]
    public void Can_create_instance_by_ctor_successful()
    {
        var project = new Project(name, developer);

        project.ShouldNotBeNull();
        project.Name.ShouldNotBeNull();
        project.Name.ShouldNotBe("just another name");
        project.Developer.ShouldNotBeNull();
        project.Developer.ShouldNotBe("taal dc");

        project.Name.ShouldBe(name);
        project.Developer.ShouldBe(developer);

    }

    [Fact]
    public void Must_throw_a_catalogDomainException()
    {
        var project = Project.NewProject();

        Assert.Throws<CatalogDomainException>(() => project.SetName(""));
    }

    [Fact]
    public void Can_add_property_successful()
    {
        var project = new Project(name, developer);
        
        project.UpsertProperty("one tolentino east residences", 1000, null);
        project.Properties.ShouldNotBeNull();
        project.Properties.Count.ShouldBe(1);
    }

    [Fact]
    public void Can_remove_property()
    {
        var project = CreateProjectWithOneProperty();
    
        var remove = project.Properties.SingleOrDefault(x => x.Name == "one tolentino east residences");

        project.RemoveProperty(remove.Id);

        project.Properties.Any(x => x.Id == remove.Id);

    }

    private Project CreateProjectWithOneProperty()
    {
        var project = new Project(name, developer);
        
        project.UpsertProperty("one tolentino east residences", 1000, null);
        return project;
    }
}