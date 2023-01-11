using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUnitSequenceStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "unitseq",
                schema: "catalog");

            migrationBuilder.CreateSequence<int>(
                name: "unitseq",
                schema: "catalog",
                startValue: 2000L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "unitseq",
                schema: "catalog");

            migrationBuilder.CreateSequence(
                name: "unitseq",
                schema: "catalog",
                incrementBy: 10);
        }
    }
}
