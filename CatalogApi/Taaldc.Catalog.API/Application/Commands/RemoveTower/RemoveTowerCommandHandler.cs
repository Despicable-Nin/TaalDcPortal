using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Application.Commands.RemoveTower;

public class RemoveTowerCommandHandler : IRequestHandler<RemoveTowerCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public RemoveTowerCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(RemoveTowerCommand request, CancellationToken cancellationToken)
    {
        _repository.RemoveTower(request.PropertyId, request.TowerId);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(request.TowerId);
    }
}