using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IUnitReplicaRepository : IRepository<UnitReplica>
{
    UnitReplica AddASync(UnitReplica unitReplica);
    UnitReplica Update(UnitReplica unitReplica);
    UnitReplica? GetById(int id);
}