using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBalconyAreaToUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BalconyArea",
                schema: "catalog",
                table: "unit",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalconyArea",
                schema: "catalog",
                table: "unit");
        }
    }
}
