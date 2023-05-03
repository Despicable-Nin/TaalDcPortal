using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterspSyncUnitStatusWithCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp =  @"USE [taaldb_sales]
                        GO
                        SET ANSI_NULLS ON
                        GO
                        SET QUOTED_IDENTIFIER ON
                        GO
                        ALTER PROCEDURE [sales].[spSyncUnitStatusWithCatalog]
	                        @UnitId INT,
	                        @UnitStatusId INT
                        AS

                        BEGIN
	                            SET NOCOUNT ON;

	                            --Test Data
	                            --DECLARE @UnitId INT = 1

	                            UPDATE catalogunit
		                        SET catalogunit.UnitStatus = @UnitStatusId
	                            --SELECT *
	                            FROM taaldb_admin.[catalog].[unit] catalogunit
	                            WHERE catalogunit.Id = @UnitId

                        END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
