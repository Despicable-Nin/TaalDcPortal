using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamebrokerfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Broker",
                schema: "sales",
                table: "order",
                newName: "Broker_Name");

            migrationBuilder.AddColumn<string>(
                name: "Broker_Email",
                schema: "sales",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Broker_Email",
                schema: "sales",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "Broker_Name",
                schema: "sales",
                table: "order",
                newName: "Broker");
        }
    }
}
