using Microsoft.EntityFrameworkCore;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly CatalogDbContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public ProjectRepository(CatalogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Project Add(Project project) 
    {
        if (project.IsTransient())
        {
            return _context.Projects.Add(project).Entity;
        }

        throw new CatalogDomainException(nameof(ProjectRepository.Add), new Exception("Data already exists."));
    }

    public Project Update(Project project) => _context.Projects.Update(project).Entity;
    public async Task<Project> GetAsync(int id)
    {
        return _context.Projects
            .Include(i => i.Properties)
            .ThenInclude(i => i.Towers)
            .ThenInclude(i => i.Floors)
            .ThenInclude(i => i.Units)
            .FirstOrDefault(i => i.Id == id);
        //.ThenInclude(i => i.Fl)
    }
}