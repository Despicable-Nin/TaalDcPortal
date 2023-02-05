using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderCorrelationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderCorrelationId",
                schema: "sales",
                table: "order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_OrderCorrelationId",
                schema: "sales",
                table: "order",
                column: "OrderCorrelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_order_OrderCorrelationId",
                schema: "sales",
                table: "order",
                column: "OrderCorrelationId",
                principalSchema: "sales",
                principalTable: "order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_order_OrderCorrelationId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_OrderCorrelationId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "OrderCorrelationId",
                schema: "sales",
                table: "order");
        }
    }
}
