using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.API.Application.Commands.UpsertUnitType;

public class UpsertUnitTypeCommandHandler : IRequestHandler<UpsertUnitTypeCommand, CommandResult>
{
    private readonly IUnitTypeRepository _repository;

    public UpsertUnitTypeCommandHandler(IUnitTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertUnitTypeCommand request, CancellationToken cancellationToken)
    {
        UnitType unitType = default;

        if (request.UnitTypeId.HasValue)
        {
            unitType = await _repository.GetAsync(request.UnitTypeId.Value);

            if (unitType == default) return CommandResult.Failed(request.UnitTypeId, typeof(UnitType));

            unitType = new UnitType(request.UnitTypeId.Value, request.Name, request.ShortCode);

            _repository.Update(unitType);
        }
        else
        {
            unitType = new UnitType(0, request.Name, request.ShortCode);

            _repository.Add(unitType);
        }

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(unitType.Id);
    }
}