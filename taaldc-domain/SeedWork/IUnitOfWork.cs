namespace taaldc_domain.SeedWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}