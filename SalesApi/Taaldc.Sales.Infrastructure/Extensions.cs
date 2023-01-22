using MediatR;
using SeedWork;

namespace Taaldc.Sales.Infrastructure;

public static class Extensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, SalesDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<DomainEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}