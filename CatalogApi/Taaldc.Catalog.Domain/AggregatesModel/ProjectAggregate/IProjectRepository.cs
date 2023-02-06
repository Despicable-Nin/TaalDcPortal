using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public interface IProjectRepository : IRepository<Project>
{
    Project Add(Project project);
    Project Update(Project project);

    Task<Project> GetAsync(int id);
    IEnumerable<Project> GetListAsync();

    Task<Property> GetPropertyAsync(int propertyId);
    Task<Property> GetPropertyByNameAsync(string name);
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
    Unit AddUnit(int floorId, int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea, double balconyArea, string remarks);
    void RemoveUnit(int floorId, int roomId);
    Unit UpdateUnit(Unit unit);
}