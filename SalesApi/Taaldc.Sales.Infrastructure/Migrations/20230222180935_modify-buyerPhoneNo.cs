using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifybuyerPhoneNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_buyer_PhoneNo",
                schema: "sales",
                table: "buyer");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_PhoneNo",
                schema: "sales",
                table: "buyer",
                column: "PhoneNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_buyer_PhoneNo",
                schema: "sales",
                table: "buyer");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_PhoneNo",
                schema: "sales",
                table: "buyer",
                column: "PhoneNo",
                unique: true);
        }
    }
}
