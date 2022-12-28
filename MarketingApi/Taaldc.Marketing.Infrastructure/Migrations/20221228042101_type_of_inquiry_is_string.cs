using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Marketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class typeofinquiryisstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inquiry_inqurytype_InquiryType",
                schema: "marketing",
                table: "inquiry");

            migrationBuilder.DropIndex(
                name: "IX_inquiry_InquiryType",
                schema: "marketing",
                table: "inquiry");

            migrationBuilder.DropColumn(
                name: "InquiryType",
                schema: "marketing",
                table: "inquiry");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfInquiry",
                schema: "marketing",
                table: "inquiry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfInquiry",
                schema: "marketing",
                table: "inquiry");

            migrationBuilder.AddColumn<int>(
                name: "InquiryType",
                schema: "marketing",
                table: "inquiry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inquiry_InquiryType",
                schema: "marketing",
                table: "inquiry",
                column: "InquiryType");

            migrationBuilder.AddForeignKey(
                name: "FK_inquiry_inqurytype_InquiryType",
                schema: "marketing",
                table: "inquiry",
                column: "InquiryType",
                principalSchema: "marketing",
                principalTable: "inqurytype",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
