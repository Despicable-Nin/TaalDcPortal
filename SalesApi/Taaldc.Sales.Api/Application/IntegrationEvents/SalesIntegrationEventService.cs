using System.Data.Common;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Application.IntegrationEvents;

public class SalesIntegrationEventService : ISalesIntegrationEventService
{
    private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
    private readonly IEventBus _eventBus;
    private readonly SalesDbContext _salesDbContext;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly ILogger<SalesIntegrationEventService> _logger;

    public SalesIntegrationEventService(
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
        IEventBus eventBus,
        SalesDbContext salesDbContext,
        ILogger<SalesIntegrationEventService> logger)
    {
        _integrationEventLogServiceFactory = integrationEventLogServiceFactory ??
                                             throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _salesDbContext = salesDbContext ?? throw new ArgumentNullException(nameof(salesDbContext));
        _eventLogService = _integrationEventLogServiceFactory(_salesDbContext.Database.GetDbConnection());
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task PublishEventsThroughtEventBusAsync(Guid transactionId)
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

        await _eventLogService.SaveEventAsync(evt, _salesDbContext.GetCurrentTransaction());
    }
}