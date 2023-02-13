using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class paymentoption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_orderstatus_StatusId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_StatusId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropSequence(
                name: "paymentypeseq",
                schema: "sales");

            migrationBuilder.CreateSequence(
                name: "paymentoptionseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "paymenttypeseq",
                schema: "sales",
                incrementBy: 10);

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

            migrationBuilder.CreateTable(
                name: "paymentoption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentoption", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_orderstatus_PaymentOptionId",
                schema: "sales",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_paymentoption_PaymentOptionId1",
                schema: "sales",
                table: "order");

            migrationBuilder.DropTable(
                name: "paymentoption");

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

            migrationBuilder.DropSequence(
                name: "paymentoptionseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymenttypeseq",
                schema: "sales");

            migrationBuilder.CreateSequence(
                name: "paymentypeseq",
                schema: "sales",
                incrementBy: 10);

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
    }
}
