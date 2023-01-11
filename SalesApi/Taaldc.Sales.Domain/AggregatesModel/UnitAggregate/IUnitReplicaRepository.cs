using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IUnitReplicaRepository : IRepository<UnitReplica>
{
    Task<UnitReplica> AddASync(UnitReplica unitReplica);
}