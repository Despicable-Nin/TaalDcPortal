using SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate
{
    public interface IUnitTypeRepository : IRepository<UnitType>
    {
        UnitType Add(UnitType unitType);
        UnitType Update(UnitType unitType);
        Task<UnitType> GetAsync(int id);
    }
}
