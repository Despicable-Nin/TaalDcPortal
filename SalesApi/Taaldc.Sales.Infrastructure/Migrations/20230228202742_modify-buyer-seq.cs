using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifybuyerseq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "buyerseq",
                schema: "sales");

            migrationBuilder.CreateSequence<int>(
                name: "buyerseq",
                schema: "sales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "buyerseq",
                schema: "sales");

            migrationBuilder.CreateSequence(
                name: "buyerseq",
                schema: "sales",
                incrementBy: 10);
        }
    }
}
