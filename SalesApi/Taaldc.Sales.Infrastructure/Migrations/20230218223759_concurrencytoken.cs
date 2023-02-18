using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class concurrencytoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpouseId",
                schema: "sales",
                table: "buyer",
                newName: "PartnerId");

            migrationBuilder.AddColumn<byte>(
                name: "TimeStamp",
                schema: "sales",
                table: "buyer",
                type: "tinyint",
                rowVersion: true,
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                schema: "sales",
                table: "buyer");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                schema: "sales",
                table: "buyer",
                newName: "SpouseId");
        }
    }
}
