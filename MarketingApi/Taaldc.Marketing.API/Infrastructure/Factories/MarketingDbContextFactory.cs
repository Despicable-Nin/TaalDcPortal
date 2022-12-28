using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Taaldc.Marketing.Infrastructure;

namespace Taaldc.Marketing.API.Infrastructure.Factories;

public class MarketingDbContextFactory : IDesignTimeDbContextFactory<MarketingDbContext>
{
    public MarketingDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<MarketingDbContext>();
        optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Taaldc.Marketing.Infrastructure"));

        return new MarketingDbContext(optionsBuilder.Options, new NoMediator());
    }
}

internal class NoMediator : IMediator
{
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        return default;
    }

    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
    {
        return default;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        return Task.CompletedTask;
    }

    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<TResponse>(default);
    }

    public Task<object> Send(object request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(default(object));
    }
}