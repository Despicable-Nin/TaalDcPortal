using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Common.Models;

namespace Taaldc.Catalog.API.Application.Queries.Towers
{
    public interface ITowerQueries
    {
        Task<PaginationQueryResult<TowerDTO>> GetActiveTowers(
             string filter,
             string sortBy,
             SortOrderEnum sortOrder,
             int pageNumber = 1,
             int pageSize = 10
         );

        Task<TowerDTO> GetTowerById(int id);
    }


    public class TowerQueries : ITowerQueries
    {
        private readonly string _connectionString;
        public TowerQueries(string connectionString)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
                ? throw new ArgumentNullException(nameof(connectionString))
                : connectionString;
        }

        public async Task<PaginationQueryResult<TowerDTO>> GetActiveTowers(string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
        {
            var query = $"SELECT DISTINCT(t.Id)" +
                $",p.[Id] AS PropertyId" +
                $",p.[Name] AS PropertyName" +
                $",t.[Name] AS TowerName" +
                $",t.[Address]" +
                $",COUNT(u.[Id]) AS Units " +
                $"FROM catalog.tower t " +
                $"JOIN catalog.property p " +
                $"ON t.PropertyId = p.Id " +
                $"LEFT JOIN catalog.floors f " +
                $"ON f.TowerId = t.Id " +
                $"LEFT JOIN catalog.unit u " +
                $"ON u.FloorId = f.Id AND u.IsActive = '1' " +
                $"AND u.ScenicViewId > 1 " +
                $"WHERE t.IsActive = '1'  " +
                $"AND (t.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%'  OR " +
                $"p.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%' ) " +
                $"GROUP BY t.Id, p.Id, p.[Name], t.[Name], t.[Address] " +
                $"ORDER BY {PropertyHelper.GetTowerSorterName(sortBy, sortOrder)} " +
                $"OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(CancellationToken.None);

            var result = await connection.QueryAsync<TowerDTO>(query);

            var countQuery = $"SELECT COUNT(*) " +
                $"FROM catalog.tower t " +
                $"JOIN catalog.property p " +
                $"ON t.PropertyId = p.Id " +
                $"WHERE t.IsActive = '1'  " +
                $"AND (t.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%'  OR " +
                $"p.[Name] LIKE '%' + ISNULL('{filter}',t.[Name]) + '%' )";

            var temp = await connection.QueryAsync<int>(countQuery);

            return new PaginationQueryResult<TowerDTO>(pageSize, pageNumber, temp.SingleOrDefault(), result);
        }

        public Task<TowerDTO> GetTowerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
