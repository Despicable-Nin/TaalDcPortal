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

    Task<Property> GetPropertyAsync(int propertyId);
    Property UpdateProperty(Property property);

    Task<Tower> GetTowerAsync(int towerId);
    Tower AddTower(int propertyId, string name, string address);
    void RemoveTower(int propertyId, int towerId);
    Tower UpdateTower(Tower tower);

    Task<Floor> GetFloorAsync(int floorId);
    Floor AddFloor(int towerId, string name, string description);
    void RemoveFloor(int towerId, int floorId);
    Floor UpdateFloor(Floor floor);

    Task<Unit> GetUnitAsync(int unitId);
    Unit AddUnit(int floorId, int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea);
    void RemoveUnit(int floorId, int roomId);
}