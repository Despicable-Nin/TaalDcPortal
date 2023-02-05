using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class incrementhilofix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.CreateSequence<int>(
                name: "orderseq",
                schema: "sales",
                startValue: 1000L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationExpiresOn",
                schema: "sales",
                table: "order",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationExpiresOn",
                schema: "sales",
                table: "order");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "sales",
                incrementBy: 10);
        }
    }
}
