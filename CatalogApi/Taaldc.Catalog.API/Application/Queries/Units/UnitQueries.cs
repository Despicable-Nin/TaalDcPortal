﻿using Dapper;
using Microsoft.Data.SqlClient;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries.Units;

namespace Taaldc.Catalog.API.Application.Queries;

public class UnitQueries : IUnitQueries
{
    private const string UNIT_QUERY =
        "SELECT u.Id ,u.IsActive, p.[Id] AS PropertyId ,p.[Name] AS PropertyName ,t.[Id] AS TowerId,t.[Name] AS TowerName ,u.UnitType AS UnitTypeId,ut.ShortCode AS UnitType,u.ScenicViewId AS ScenicViewId,sv.Name AS ScenicView,u.FloorId AS FloorId,f.[Name] AS FloorName,u.FloorArea+u.BalconyArea AS TotalArea,u.FloorArea,u.BalconyArea,u.Identifier,u.Price, u.Tower, u.UnitStatus AS UnitStatusId,us.[Name] AS UnitStatus FROM catalog.unit u JOIN catalog.floors f ON u.FloorId = f.Id JOIN catalog.tower t ON f.TowerId = t.Id JOIN catalog.property p ON t.PropertyId = p.Id JOIN catalog.unittype ut ON u.UnitType = ut.Id JOIN catalog.scenicview sv ON u.ScenicViewId = sv.Id JOIN catalog.unitstatus us ON u.UnitStatus = us.Id ";

    private readonly string _connectionString;

    public UnitQueries(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentNullException(nameof(connectionString))
            : connectionString;
    }

    public async Task<PaginationQueryResult<UnitQueryResult>> GetActiveUnitsAsync(
        string filter,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int? statusId,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = UNIT_QUERY +
                    "WHERE u.IsActive = '1' AND f.IsActive = '1' AND t.IsActive = '1' AND p.IsActive = '1' " +
                    $"AND u.Identifier LIKE '%' + ISNULL('{filter}',u.Identifier) + '%' " +
                    $"AND f.Id = ISNULL({(floorId > 0 ? $"'{floorId}'" : "NULL")},f.Id) " +
                    $"AND ut.Id = ISNULL({(unitTypeId > 0 ? $"'{unitTypeId}'" : "NULL")},ut.Id) " +
                    $"AND sv.Id = ISNULL({(viewId > 0 ? $"'{viewId}'" : "NULL")}, sv.Id) " +
                    $"AND us.Id = ISNULL({(statusId > 0 ? $"'{statusId}'" : "NULL")},us.Id) " +
                    $"ORDER BY {PropertyHelper.GetUnitSorterName(sortBy, sortOrder)} " +
                    $"OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<UnitQueryResult>(query);

        var countQuery = "WITH unit_cte AS (SELECT u.Id," +
                         "f.Id AS FloorId," +
                         "t.Id AS TowerId," +
                         "p.Id AS PropertyId," +
                         "ut.Id AS UnitTypeId," +
                         "sv.Id AS ScenicViewId," +
                         "us.Id AS UnitStatusId, " +
                         "u.IsActive " +
                         "FROM catalog.unit u " +
                         "JOIN catalog.floors f " +
                         "ON u.FloorId = f.Id " +
                         "JOIN catalog.tower t " +
                         "ON f.TowerId = t.Id " +
                         "JOIN catalog.property p " +
                         "ON t.PropertyId = p.Id " +
                         "JOIN catalog.unittype ut " +
                         "ON u.UnitType = ut.Id " +
                         "JOIN catalog.scenicview sv " +
                         "ON u.ScenicViewId = sv.Id " +
                         "JOIN catalog.unitstatus us " +
                         "ON u.UnitStatus = us.Id " +
                         "WHERE u.IsActive = '1' " +
                         $"AND u.Identifier LIKE '%' + COALESCE('{filter}',u.Identifier) + '%' " +
                         $"AND u.FloorId = COALESCE({(floorId > 0 ? $"'{floorId}'" : "NULL")},u.FloorId) " +
                         $"AND u.UnitType = COALESCE({(unitTypeId > 0 ? $"'{unitTypeId}'" : "NULL")},u.UnitType) " +
                         $"AND u.ScenicViewId = COALESCE({(viewId > 0 ? $"'{viewId}'" : "NULL")}, u.ScenicViewId) " +
                         $"AND u.UnitStatus = COALESCE({(statusId > 0 ? $"'{statusId}'" : "NULL")},u.UnitStatus))  " +
                         "SELECT TOP 1 COUNT(u.Id) FROM unit_cte u";

        var temp = await connection.QueryAsync<int>(countQuery);

        return new PaginationQueryResult<UnitQueryResult>(pageSize, pageNumber, temp.SingleOrDefault(), result);
    }


    public async Task<UnitQueryResult> GetUnitByIdAsync(int unitId)
    {
        var query = UNIT_QUERY + $" WHERE u.Id = {unitId}";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);
        var result = await connection.QueryAsync<UnitQueryResult>(query);

        return result.SingleOrDefault();
    }

    public async Task<IEnumerable<UnitColorScheme>> GetUnitColorSchemeByFloorIdAsync(int floorId)
    {
        var query = @$"SELECT
	                F.[Name] [Floor]
	                ,F.[FloorPlanFilePath] [FilePath]
	                ,U.Id [UnitId]
                    ,PARSENAME(REPLACE([Identifier] , '-' , '.'),1) [Unit]
                    ,[UnitStatus]
                    ,(CASE
		                WHEN [UnitStatus] = 1 THEN '#00FF01'
		                WHEN [UnitStatus] = 2 THEN '#FE0000'
		                WHEN [UnitStatus] = 3 THEN '#FEA500'
		                ELSE 'rgba(87,87,87,1)' 
                    END) [Color]
                  FROM [taaldb_admin].[catalog].[unit] U
                  JOIN [taaldb_admin].[catalog].[floors] F ON F.Id = U.FloorId
                  WHERE F.[Id] = {floorId}";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<UnitColorScheme>(query);

        return result;
    }

    public async Task<PaginatedAvailableUnitsQueryResult> GetAvailableUnitsAsync(
        string? filter,
        int? unitTypeId,
        int? viewId,
        int? floorId,
        string location,
        int min = 0,
        int max = 999999999,
        int pageSize = 20, int pageNumber = 1)
    {
        var query =
            " SELECT U.[Id], U.[Identifier], U.[Price], CONVERT(DECIMAL(10,2),U.[FloorArea] + ISNULL(U.[BalconyArea],0)) AS FloorArea, F.Name [Floor], F.[Description] [FloorDesc], S.Name [View], US.Name [Status], UT.Name [Type], UT.ShortCode [TypeCode], U.Remarks [Remarks]";
        query += " FROM [taaldb_admin].[catalog].[unit] U JOIN [taaldb_admin].[catalog].floors F ON U.FloorId = F.Id ";
        query += " JOIN [taaldb_admin].[catalog].scenicview S ON U.ScenicViewId = S.Id ";
        query += " JOIN [taaldb_admin].[catalog].unitstatus US ON U.UnitStatus = US.Id ";
        query += " JOIN taaldb_admin.[catalog].unittype UT ON U.UnitType = UT.Id ";

        List<string> clauses = new();

        if (unitTypeId.HasValue) clauses.Add($"UT.Id = {unitTypeId.Value}");


        if (viewId.HasValue) clauses.Add($"S.Id = {viewId.Value}");

        if (floorId.HasValue) clauses.Add($"F.Id = {floorId.Value}");

        if (!string.IsNullOrEmpty(location)) clauses.Add($"U.Remarks LIKE '%{location}%'");

        if (!string.IsNullOrEmpty(filter)) clauses.Add($"U.Identifier LIKE '%{filter}%'");

        clauses.Add($"(U.Price BETWEEN {min} AND {max})");
        clauses.Add("US.Id = 1");

        var where = string.Empty;
        if (clauses.Any()) where = " WHERE " + string.Join(" AND ", clauses.ToArray());

        if (!string.IsNullOrWhiteSpace(where)) query += where;

        query +=
            $" ORDER BY F.Id, TRY_CAST(REPLACE(REPLACE(U.Identifier, 'P', ''),F.[Name] + '-','') AS INT), U.Id OFFSET {(pageNumber - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY ";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<AvailableUnitQueryResult>(query);

        var countQuery = " SELECT COUNT(*) [Total] ";
        countQuery +=
            " FROM [taaldb_admin].[catalog].[unit] U JOIN [taaldb_admin].[catalog].floors F ON U.FloorId = F.Id ";
        countQuery += " JOIN [taaldb_admin].[catalog].scenicview S ON U.ScenicViewId = S.Id ";
        countQuery += " JOIN [taaldb_admin].[catalog].unitstatus US ON U.UnitStatus = US.Id ";
        countQuery += " JOIN taaldb_admin.[catalog].unittype UT ON U.UnitType = UT.Id ";
        countQuery += where;

        var temp = await connection.QueryAsync<int>(countQuery);

        return new PaginatedAvailableUnitsQueryResult(pageSize, pageNumber, temp.Single(), result.ToArray());
    }

    public async Task<IEnumerable<UnitTypeAvailabilityQueryResult>> GetUnitTypeAvailabilityByTowerIdAsync(int towerId)
    {
        var query = "SELECT f.TowerId," +
                    "ut.Id AS UnitTypeId," +
                    "ut.ShortCode AS UnitTypeCode," +
                    "ut.[Name] AS UnitType," +
                    "ISNULL(MIN(CONVERT(DECIMAL(10,2),u.FloorArea + u.BalconyArea)),0) AS MinFloorArea," +
                    "ISNULL(MAX(CONVERT(DECIMAL(10,2),u.FloorArea + u.BalconyArea,2)),0) AS MaxFloorArea," +
                    "ISNULL(MIN(CONVERT(DECIMAL(10,2),u.Price)),0) AS MinPrice," +
                    "ISNULL(MAX(CONVERT(DECIMAL(10,2),u.Price,2)),0) AS MaxPrice," +
                    "COUNT(u.UnitType) AS Available " +
                    "FROM [taaldb_admin].catalog.unittype ut " +
                    "LEFT JOIN [taaldb_admin].catalog.unit u " +
                    "ON u.UnitType = ut.Id " +
                    "JOIN [taaldb_admin].catalog.floors f " +
                    "ON f.Id = u.FloorId " +
                    $"WHERE f.TowerId = {towerId} " +
                    "AND u.UnitStatus = 1 " +
                    "GROUP BY u.UnitType, ut.[Name]," +
                    "ut.ShortCode,ut.Id,f.TowerId " +
                    "ORDER BY ut.Id";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        var result = await connection.QueryAsync<UnitTypeAvailabilityQueryResult>(query);

        return result;
    }
    
    
}