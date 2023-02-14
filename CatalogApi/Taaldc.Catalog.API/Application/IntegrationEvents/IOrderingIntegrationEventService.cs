using System.Data.Common;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Taaldc.Catalog.Infrastructure;

namespace Taaldc.Catalog.API.Application.IntegrationEvents;

public interface ICatalogIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}


public class CatalogIntegrationEventService : ICatalogIntegrationEventService
{
    private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
    private readonly IEventBus _eventBus;
    private readonly CatalogDbContext _catalogDbContext;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly ILogger<CatalogIntegrationEventService> _logger;

    public CatalogIntegrationEventService(
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
        IEventBus eventBus,
        CatalogDbContext catalogDbContext,
        ILogger<CatalogIntegrationEventService> logger)
    {
        _integrationEventLogServiceFactory = integrationEventLogServiceFactory ??
                                             throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _catalogDbContext = catalogDbContext ?? throw new ArgumentNullException(nameof(catalogDbContext));
        _eventLogService = _integrationEventLogServiceFactory(_catalogDbContext.Database.GetDbConnection());
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        foreach (var logEvt in pendingLogEvents)
        {
            var nameSpace = typeof(Program).Namespace;
            var appName = nameSpace.Substring(nameSpace.LastIndexOf('.', nameSpace.LastIndexOf('.') - 1) + 1);

            _logger.LogInformation(
                "----------Publishing integration event:{IntegrationEventId} from {AppName} - {@IntegrationEvent}",
                logEvt.EventId, appName, logEvt.IntegrationEvent);

            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);

                _eventBus.Publish(logEvt.IntegrationEvent);

                await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event:{IntegrationEventId} form {AppName}",
                    logEvt.EventId, appName);

                await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }
    }

    public async Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

        await _eventLogService.SaveEventAsync(evt, _catalogDbContext.GetCurrentTransaction());
    }
}