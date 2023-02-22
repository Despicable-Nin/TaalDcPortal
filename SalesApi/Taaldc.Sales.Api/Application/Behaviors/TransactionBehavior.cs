using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;
using Taaldc.Sales.Api.Application.IntegrationEvents;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.API.Application.Behaviors;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly SalesDbContext _dbContext;

    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
    private readonly ISalesIntegrationEventService _salesIntegrationEventService;

    public TransactionBehaviour(SalesDbContext dbContext,
        ISalesIntegrationEventService salesIntegrationEventService,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(SalesDbContext));
        _salesIntegrationEventService = salesIntegrationEventService ?? throw new ArgumentException(nameof(salesIntegrationEventService));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();


        try
        {
             if (_dbContext.HasActiveTransaction)
             {
                 return await next();
             }
            
             var strategy = _dbContext.Database.CreateExecutionStrategy();

             await strategy.ExecuteAsync(async () =>
             {
                 Guid transactionId;
            
                 using var transaction = await _dbContext.BeginTransactionAsync();
                 using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                 {
                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                     await _dbContext.CommitTransactionAsync(transaction);
            
                     transactionId = transaction.TransactionId;
                 }
                 await _salesIntegrationEventService.PublishEventsThroughtEventBusAsync(transactionId);
             });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}