using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedwarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "scenicviewseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "unitstatusseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "catalog",
                table: "unitstatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "catalog",
                table: "unit",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "catalog",
                table: "scenicview",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "scenicviewseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "unitstatusseq",
                schema: "catalog");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "catalog",
                table: "unitstatus",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "catalog",
                table: "unit",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "catalog",
                table: "scenicview",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
