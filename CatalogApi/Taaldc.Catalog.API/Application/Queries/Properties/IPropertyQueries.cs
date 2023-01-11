using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Properties
{
    public interface IPropertyQueries
    {
        Task<PaginationQueryResult<PropertyDTO>> GetActiveProperties(
            string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10
        );

        Task<PropertyDTO> GetPropertyById(int id);
    }


    public class PropertyQueries : IPropertyQueries
    {
        private readonly string _connectionString;

        public PropertyQueries(string connectionString)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
                ? throw new ArgumentNullException(nameof(connectionString))
                : connectionString;
        }

        public async Task<PaginationQueryResult<PropertyDTO>> GetActiveProperties(
            string filter, 
            string sortBy,
            SortOrderEnum sortOrder = SortOrderEnum.ASC,
            int pageNumber = 1, 
            int pageSize = 10)
        {
            var query = $"SELECT " +
                $"p.Id," +
                $"p.[Name] AS PropertyName," +
                $"p.[LandArea]," +
                $"p.[ProjectId]," +
                $"COUNT(t.[Id]) AS Towers " +
                $"FROM [taaldb_admin].[catalog].property p " +
                $"LEFT JOIN [taaldb_admin].[catalog].[tower] t " +
                $"ON t.PropertyId = p.Id and t.IsActive = '1'" +
                $"WHERE p.IsActive = '1' AND p.[Name] LIKE '%' + ISNULL('{filter}',p.[Name]) + '%' " +
                $"GROUP BY p.Id, p.Name, p.LandArea, p.[ProjectId], t.Id, t.IsActive " +
                $"ORDER BY {PropertyHelper.GetPropertySorterName(sortBy, sortOrder)} OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(CancellationToken.None);

            var result = await connection.QueryAsync<PropertyDTO>(query);

            var countQuery = $"SELECT COUNT(*) " +
                $"FROM [taaldb_admin].[catalog].property p " +
                $"LEFT JOIN [taaldb_admin].[catalog].[tower] t " +
                $"ON t.PropertyId = p.Id AND t.IsActive = '1' " +
                $"WHERE p.IsActive = '1' AND p.[Name] LIKE '%' + ISNULL('{filter}',p.[Name]) + '%'";

            var temp = await connection.QueryAsync<int>(countQuery);

            return new PaginationQueryResult<PropertyDTO>(pageSize, pageNumber, temp.SingleOrDefault(), result);
        }

        public async Task<PropertyDTO> GetPropertyById(int id)
        {
            var query = $"SELECT p.Id," +
                $"p.[Name] AS PropertyName," +
                $"p.[LandArea]," +
                $"p.[ProjectId]," +
                $"COUNT(t.[Id]) AS Towers " +
                $"FROM catalog.property p  " +
                $"LEFT JOIN catalog.tower t " +
                $"ON t.PropertyId = p.Id AND t.IsActive = '1'" +
                $"WHERE p.Id = '{id}' " +
                $"GROUP BY p.Id, p.Name, p.[ProjectId], p.LandArea, t.Id, t.IsActive";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(CancellationToken.None);

            try {
                var result = await connection.QueryFirstAsync<PropertyDTO>(query);
                return result;
            }
            catch(Exception err)
            {
                return null;
            }
        }
    }
}
