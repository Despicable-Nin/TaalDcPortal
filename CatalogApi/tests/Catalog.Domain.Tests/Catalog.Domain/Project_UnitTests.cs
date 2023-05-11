using Shouldly;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.Exceptions;

namespace Catalog.Domain.Tests.Catalog.Domain;

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

        project.AddProperty("one tolentino east residences", 1000);
        project.Properties.ShouldNotBeNull();
        project.Properties.Count.ShouldBe(1);
    }

    [Fact]
    public void Can_remove_property()
    {
        var project = CreateProjectWithOneProperty();

        var remove = project.Properties.SingleOrDefault(x => x.Name == "one tolentino east residences");

        remove.ShouldNotBeNull();

        project.RemoveProperty(remove.Id);

        project.Properties.Any(x => x.Id == remove.Id);
    }

    [Fact]
    public void TowerPropertyFromUnitShouldBeSet()
    {
        Unit unit = new(1, 1, "X-XXX", 20M, 0, 0, "Tower One");
        unit.Tower.ShouldBe("Tower One");
        unit.Tower.ShouldNotBeNullOrEmpty();
    }
    
    [Fact]
    public void TowerPropertyFromUnitShouldBeSetByDefault()
    {
        Unit unit = new(1, 1, "X-XXX", 20M, 0, 0);
        unit.Tower.ShouldBe("N/A");
        unit.Tower.ShouldNotBeNullOrEmpty();
    }

    private Project CreateProjectWithOneProperty()
    {
        var project = new Project(name, developer);

        project.AddProperty("one tolentino east residences", 1000);
        return project;
    }
}