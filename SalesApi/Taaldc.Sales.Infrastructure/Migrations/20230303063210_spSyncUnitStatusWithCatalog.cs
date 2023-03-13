using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class spSyncUnitStatusWithCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE taaldb_sales
                        GO

                        CREATE PROCEDURE [sales].[spSyncUnitStatusWithCatalog]
	                        @UnitId INT
                        AS

                        BEGIN
	                            SET NOCOUNT ON;

	                            --Test Data
	                            --DECLARE @UnitId INT = 1

	                            UPDATE catalogunit
		                        SET catalogunit.UnitStatus = salesunit.UnitStatusId
	                            --SELECT *
	                            FROM taaldb_admin.[catalog].[unit] catalogunit
	                            JOIN sales.unitreplica salesunit
	                            ON salesunit.Id = catalogunit.Id
	                            WHERE salesunit.Id = @UnitId

                        END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
