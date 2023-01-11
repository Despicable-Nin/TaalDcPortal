using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRemarksToUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "catalog",
                table: "unit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "catalog",
                table: "unit");
        }
    }
}
