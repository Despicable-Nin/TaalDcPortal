using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUnitTypeSeq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "unittypeseq",
                schema: "catalog");

            migrationBuilder.CreateSequence<int>(
                name: "unittypeseq",
                schema: "catalog",
                startValue: 9L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "unittypeseq",
                schema: "catalog");

            migrationBuilder.CreateSequence(
                name: "unittypeseq",
                schema: "catalog",
                incrementBy: 10);
        }
    }
}
