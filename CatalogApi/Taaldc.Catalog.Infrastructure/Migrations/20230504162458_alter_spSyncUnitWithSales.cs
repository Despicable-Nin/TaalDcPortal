using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterspSyncUnitWithSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE [taaldb_admin]
						GO

						SET ANSI_NULLS ON
						GO
						SET QUOTED_IDENTIFIER ON
						GO

						ALTER PROCEDURE [catalog].[spSyncUnitWithSales] 
							@UnitId INT 
						AS

						BEGIN
							SET NOCOUNT ON;

							/*TEST DATA
							DECLARE @UnitId INT = 10000
							*/

							IF EXISTS(SELECT 1 FROM [taaldb_sales].[sales].[unitreplica] WHERE Id = @UnitId)
								BEGIN
									UPDATE salesunit
									SET  salesunit.UnitTypeId = cu.UnitType
										,salesunit.UnitTypeShortCode = ut.ShortCode
										,salesunit.UnitType = ut.Name
										,salesunit.ScenicViewId = cu.ScenicViewId
										,salesunit.ScenicView = uv.Name
										,salesunit.Unit = cu.Identifier
										,salesunit.FloorId = cu.FloorId
										,salesunit.[Floor] =  uf.Name
										,salesunit.UnitArea = cu.FloorArea
										,salesunit.BalconyArea = cu.BalconyArea
										,salesunit.SellingPrice = cu.Price
										,salesunit.UnitStatusId = cu.UnitStatus
										,salesunit.UnitStatus = us.Name
										,salesunit.TowerName = cu.Tower
										,salesunit.IsActive = cu.IsActive
										,salesunit.ModifiedOn = cu.ModifiedOn
										,salesunit.ModifiedBy = cu.ModifiedBy
									--SELECT *
									FROM [taaldb_sales].[sales].[unitreplica] salesunit
									JOIN [catalog].[unit] cu
									ON cu.Id = salesunit.Id
									JOIN [catalog].[unittype] ut
									ON cu.UnitType = ut.Id
									JOIN [catalog].[scenicview] uv
									ON cu.ScenicViewId = uv.Id
									JOIN [catalog].[floors] uf
									ON cu.FloorId = uf.Id
									JOIN [catalog].[unitstatus] us
									ON cu.UnitStatus = us.Id
									WHERE cu.Id = @UnitId
								END
							ELSE
							BEGIN
								INSERT INTO taaldb_sales.sales.unitreplica
								(Id,
									CreatedBy,
									CreatedOn,
									ModifiedBy,
									ModifiedOn,
									IsActive,
									PropertyId,
									TowerId,
									FloorId,
									UnitId,
									ScenicViewId,
									UnitTypeId,
									Property,
									Tower,
									Floor,
									Unit,
									ScenicView,
									UnitType,
									UnitTypeShortCode,
									UnitArea,
									BalconyArea,
									UnitStatus,
									UnitStatusId,
									OriginalPrice,
									SellingPrice,
									TowerName)
								SELECT u.Id,
										u.CreatedBy,
										u.CreatedOn,
										u.ModifiedBy,
										u.ModifiedOn,
										u.IsActive,
										p.Id          AS PropertyId,
										t.Id          AS TowerId,
										u.FloorId,
										u.Id          AS UnitId,
										u.ScenicViewId,
										u.UnitType    AS UnitTypeId,
										p.Name        AS Property,
										t.Name        AS TowerName,
										f.Name        AS Floor,
										u.Identifier  AS Unit,
										sv.Name       AS ScenicView,
										ut.Name       AS UnitType,
										ut.ShortCode  AS UnitTypeShortCode,
										u.FloorArea   AS UnitArea,
										u.BalconyArea AS BalconyArea,
										us.Name       AS UnitStatus,
										u.UnitStatus  AS UnitStatusId,
										u.Price       AS OriginalPrice,
										u.Price       AS SellingPrice,
										u.Tower	     AS TowerName
								FROM catalog.unit u
											JOIN catalog.floors f
												ON u.FloorId = f.Id
											JOIN catalog.tower t
												ON f.TowerId = t.Id
											JOIN catalog.property p
												ON t.PropertyId = p.Id
											JOIN catalog.scenicview sv
												ON u.ScenicViewId = sv.Id
											JOIN catalog.unittype ut
												ON u.UnitType = ut.Id
											JOIN catalog.unitstatus us
												ON u.UnitStatus = us.Id
								WHERE u.Id = @UnitId
							END
						END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
