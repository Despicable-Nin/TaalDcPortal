using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.Exceptions;

namespace Taaldc.Catalog.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly CatalogDbContext _context;

    public ProjectRepository(CatalogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public Project Add(Project project)
    {
        if (project.IsTransient()) return _context.Projects.Add(project).Entity;

        throw new CatalogDomainException(nameof(Add), new Exception("Project already exists."));
    }

    public Project Update(Project project)
    {
        return _context.Projects.Update(project).Entity;
    }

    public async Task<Project?> GetAsync(int id)
    {
        return await _context.Projects
            .Include(i => i.Properties)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public IEnumerable<Project> GetListAsync()
    {
        return _context.Projects.AsNoTracking().AsEnumerable();
    }

    public Property UpdateProperty(Property property)
    {
        return _context.Properties.Update(property).Entity;
    }

    public Tower AddTower(int propertyId, string name, string address)
    {
        var property = _context.Properties.Include(i => i.Towers).FirstOrDefault(i => i.Id == propertyId);

        var tower = property.AddTower(name, address);

        _context.Properties.Update(property);

        return tower;
    }

    public void RemoveTower(int propertyId, int towerId)
    {
        var property = _context.Properties.Include(i => i.Towers).FirstOrDefault(i => i.Id == propertyId);

        property.RemoveTower(towerId);

        _context.Properties.Update(property);

        var tower = _context.Towers.FirstOrDefault(i => i.Id == propertyId);

        _context.Towers.Remove(tower);
    }

    public Tower UpdateTower(Tower tower) => _context.Towers.Update(tower).Entity;

    public async Task<Floor> GetFloorAsync(int floorId)
    {
        return await _context.Floors.Include(i => i.Units).FirstOrDefaultAsync(i => i.Id == floorId);
    }

    public Floor AddFloor(int towerId, string name, string description)
    {
        var tower = _context.Towers.Include(i => i.Floors).FirstOrDefault(i => i.Id == towerId);

        var floor = tower.AddFloor(name, description);

        _context.Towers.Update(tower);

        return floor;
    }

    public void RemoveFloor(int towerId, int floorId)
    {
        var tower = _context.Towers.Include(i => i.Floors).FirstOrDefault(i => i.Id == towerId);

        tower.RemoveFloor(floorId);

        _context.Towers.Update(tower);

        var floor = _context.Floors.FirstOrDefault(i => i.Id == floorId);

        _context.Towers.Remove(tower);
    }

    public Floor UpdateFloor(Floor floor)
    {
        return _context.Floors.Update(floor).Entity;
    }

    public async Task<Unit> GetUnitAsync(int unitId) => await _context.Units.FirstOrDefaultAsync(i => i.Id == unitId);


    public Unit AddUnit(int floorId, int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea, double balconyArea, string remarks)
    {
        var floor = _context.Floors.Include(i => i.Units).FirstOrDefault(i => i.Id == floorId);

        var unit = floor.AddUnit(scenicViewId, unitTypeId, identifier, price, floorArea, balconyArea, remarks);

        _context.Floors.Update(floor);

        return unit;
    }

    public void RemoveUnit(int floorId, int roomId)
    {
        var floor = _context.Floors.Include(i => i.Units).FirstOrDefault(i => i.Id == floorId);

        floor.RemoveUnit(floorId);

        _context.Floors.Update(floor);

        var unit = _context.Units.FirstOrDefault(i => i.Id == floorId);

        _context.Units.Remove(unit);
    }

    public async Task<Property> GetPropertyAsync(int propertyId)
    {
        return await _context.Properties
            .Include(i => i.Towers)
            .FirstOrDefaultAsync(i => i.Id == propertyId);
    }

    public async Task<Tower> GetTowerAsync(int towerId)
    {
        return await _context.Towers
            .Include(i => i.Floors)
            .SingleOrDefaultAsync(i => i.Id == towerId);
    }
    

    public void RemoveProperty(int projectId, int propertyId)
    {
        var project = _context.Projects.Include(i => i.Properties).FirstOrDefault(i => i.Id == projectId);

        project.RemoveProperty(propertyId, true);

        _context.Projects.Update(project);

        var property = _context.Properties.FirstOrDefault(i => i.Id == propertyId);

        _context.Properties.Remove(property);
    }

	public Unit UpdateUnit(Unit unit)
	{
        return _context.Units.Update(unit).Entity;
	}
}