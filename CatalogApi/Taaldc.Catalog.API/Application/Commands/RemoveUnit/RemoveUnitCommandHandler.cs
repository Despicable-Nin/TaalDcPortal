using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Application.Commands.RemoveUnit;

public class RemoveUnitCommandHandler : IRequestHandler<RemoveUnitCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public RemoveUnitCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(RemoveUnitCommand request, CancellationToken cancellationToken)
    {
        _repository.RemoveUnit(request.FloorId, request.UnitId);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(request.UnitId);
    }
}