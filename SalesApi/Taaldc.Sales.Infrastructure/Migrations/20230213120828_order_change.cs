using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_unitreplica_UnitId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_UnitId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "IsRefundable",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "UnitId",
                schema: "sales",
                table: "order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                schema: "sales",
                table: "order",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsRefundable",
                schema: "sales",
                table: "order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                schema: "sales",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_order_UnitId",
                schema: "sales",
                table: "order",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_unitreplica_UnitId",
                schema: "sales",
                table: "order",
                column: "UnitId",
                principalSchema: "sales",
                principalTable: "unitreplica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
