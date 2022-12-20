using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Mvc.Application.Commands.RemoveFloor;

public class RemoveFloorCommandHandler : IRequestHandler<RemoveFloorCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public RemoveFloorCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(RemoveFloorCommand request, CancellationToken cancellationToken)
    {
        _repository.RemoveFloor(request.TowerId, request.FloorId);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(request.FloorId);
    }
}