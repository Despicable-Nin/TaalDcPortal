using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class civilstatusrecon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "civilstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "sales",
                table: "civilStatus",
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
                name: "civilstatusseq",
                schema: "sales");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "sales",
                table: "civilStatus",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
