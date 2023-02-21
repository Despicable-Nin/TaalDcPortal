using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class buyerrowversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                schema: "sales",
                table: "buyer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TimeStamp",
                schema: "sales",
                table: "buyer",
                type: "tinyint",
                rowVersion: true,
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
