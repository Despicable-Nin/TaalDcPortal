using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;
using Taaldc.Catalog.Domain.Exceptions;

namespace Taaldc.Catalog.API.Application.Commands.ChangeStatusOfUnit;

public class ChangeStatusOfUnitCommandHandler : IRequestHandler<ChangeStatusOfUnitCommand, CommandResult>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<ChangeStatusOfUnitCommandHandler> _logger;


    public ChangeStatusOfUnitCommandHandler(IProjectRepository repository, ILogger<ChangeStatusOfUnitCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(ChangeStatusOfUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await _repository.GetUnitAsync(request.UnitId);

        if (unit == default)
            throw new CatalogDomainException(nameof(ChangeStatusOfUnitCommandHandler),
                new Exception("Unit not found."));
        
        if (request.UnitStatus == unit.GetUnitStatusId())
            throw new CatalogDomainException(nameof(ChangeStatusOfUnitCommandHandler),
                new InvalidOperationException("Cannot update unit status with the same."));

        if ((int)UnitStatus.UnitIs.BLOCKED == unit.GetUnitStatusId())
        {
            throw new CatalogDomainException(nameof(ChangeStatusOfUnitCommandHandler),
                new InvalidOperationException("Cannot update a blocked unit."));
        }

        unit.SetUnitStatus(request.UnitStatus);
        unit.AddRemarks(request.Remarks);

        _repository.UpdateUnit(unit);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(unit.Id);
    }
}