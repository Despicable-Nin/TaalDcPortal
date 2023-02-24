using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transctiondate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_orderstatus_PaymentOptionId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_paymentoption_PaymentOptionId1",
                schema: "sales",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_PaymentOptionId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_PaymentOptionId1",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "PaymentOptionId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "PaymentOptionId1",
                schema: "sales",
                table: "order");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                schema: "sales",
                table: "order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_order_StatusId",
                schema: "sales",
                table: "order",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_orderstatus_StatusId",
                schema: "sales",
                table: "order",
                column: "StatusId",
                principalSchema: "sales",
                principalTable: "orderstatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_orderstatus_StatusId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_StatusId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                schema: "sales",
                table: "order");

            migrationBuilder.AddColumn<int>(
                name: "PaymentOptionId",
                schema: "sales",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentOptionId1",
                schema: "sales",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_order_PaymentOptionId",
                schema: "sales",
                table: "order",
                column: "PaymentOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_order_PaymentOptionId1",
                schema: "sales",
                table: "order",
                column: "PaymentOptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_order_orderstatus_PaymentOptionId",
                schema: "sales",
                table: "order",
                column: "PaymentOptionId",
                principalSchema: "sales",
                principalTable: "orderstatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_paymentoption_PaymentOptionId1",
                schema: "sales",
                table: "order",
                column: "PaymentOptionId1",
                principalTable: "paymentoption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
