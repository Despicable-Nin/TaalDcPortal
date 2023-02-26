using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyorderseq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDate",
                schema: "sales",
                table: "order");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.CreateSequence<int>(
                name: "orderseq",
                schema: "sales",
                startValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                schema: "sales",
                table: "order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
