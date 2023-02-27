using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

public interface IUnitTypeRepository : IRepository<UnitType>
{
    UnitType Add(UnitType unitType);
    UnitType Update(UnitType unitType);
    Task<UnitType> GetAsync(int id);
}

public interface IUnitStatusRepository : IRepository<UnitStatus>
{
}