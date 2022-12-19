using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.TowerAggregate;
using Taaldc.Catalog.Domain.SeedWork;


namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public interface IProjectRepository : IRepository<Project>
{
    Project Add(Project project);
    Project Update(Project project);
    
    Task<Project> GetAsync(int id);
    IEnumerable<Project> GetListAsync();

    Property UpdateProperty(Property property);

    Tower AddTower(int propertyId, string name, string address);
    void RemoveTower(int propertyId, int towerId);

    Floor AddFloor(int towerId, string name, string description);
    void RemoveFloor(int towerId, int floorId);

    Unit AddUnit(int floorId, int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea);
    void RemoveUnit(int floorId, int roomId);

    Task<Property> GetPropertyAsync(int propertyId);
    Task<Tower> GetTowerAsync(int towerId);
    

}