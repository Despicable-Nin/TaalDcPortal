using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hilo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "floorseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "projectseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "propertyseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "towerseq",
                schema: "catalog");

            migrationBuilder.CreateSequence<int>(
                name: "floorseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence<int>(
                name: "projectseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence<int>(
                name: "propertyseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence<int>(
                name: "towerseq",
                schema: "catalog",
                startValue: 2000L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "floorseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "projectseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "propertyseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "towerseq",
                schema: "catalog");

            migrationBuilder.CreateSequence(
                name: "floorseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "projectseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "propertyseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "towerseq",
                schema: "catalog",
                incrementBy: 10);
        }
    }
}
