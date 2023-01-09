using System.Collections.Generic;
using Taaldc.Catalog.API.Application.Queries.UnitTypes;

namespace Taaldc.Catalog.API.Application.Queries.References
{
    public interface IUnitTypeQueries 
    {
        Task<IEnumerable<UnitTypeDTO>> GetUnitTypes();
    }

    public class ReferenceQueries : IUnitTypeQueries
    {
        public ReferenceQueries()
        {

        }

        public async Task<IEnumerable<UnitTypeDTO>> GetUnitTypes()
        {
            throw new NotImplementedException();
        }
    }
}
