using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifybuyermiddlename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_buyer_FirstName_MiddleName_LastName",
                schema: "sales",
                table: "buyer");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "sales",
                table: "buyer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_FirstName_MiddleName_LastName",
                schema: "sales",
                table: "buyer",
                columns: new[] { "FirstName", "MiddleName", "LastName" },
                unique: true,
                filter: "[MiddleName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_buyer_FirstName_MiddleName_LastName",
                schema: "sales",
                table: "buyer");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "sales",
                table: "buyer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_buyer_FirstName_MiddleName_LastName",
                schema: "sales",
                table: "buyer",
                columns: new[] { "FirstName", "MiddleName", "LastName" },
                unique: true);
        }
    }
}
