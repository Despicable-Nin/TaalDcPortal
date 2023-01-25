using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Taaldc.Catalog.API.Application.Queries.ScenicViews;
using Taaldc.Catalog.API.Application.Queries.UnitTypes;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Application.Queries.References
{
    public interface IUnitTypeQueries 
    {
        Task<IEnumerable<UnitTypeQueryResult>> GetUnitTypes();
    }

    public class UnitTypeQueries : IUnitTypeQueries
    {
        private readonly string _connectionString;
        public UnitTypeQueries(string connectionString)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentNullException(nameof(connectionString))
            : connectionString;
        }

        public async Task<IEnumerable<UnitTypeQueryResult>> GetUnitTypes()
        {
            var availableViewsQuery = $"SELECT Id" +
                $",Name" +
                $",ShortCode " +
                $"FROM catalog.unittype";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(CancellationToken.None);

            var result = await connection.QueryAsync<UnitTypeQueryResult>(availableViewsQuery);

            return result;
        }
    }
}
