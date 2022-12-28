using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taaldc.Marketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "marketing");

            migrationBuilder.CreateSequence(
                name: "inquiryseq",
                schema: "marketing",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "inquirystatus",
                schema: "marketing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inquirystatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "inqurytype",
                schema: "marketing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inqurytype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "inquiry",
                schema: "marketing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    InquiryType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerSalutation = table.Column<string>(name: "Customer_Salutation", type: "nvarchar(max)", nullable: false),
                    CustomerFirstName = table.Column<string>(name: "Customer_FirstName", type: "nvarchar(max)", nullable: false),
                    CustomerLastName = table.Column<string>(name: "Customer_LastName", type: "nvarchar(max)", nullable: false),
                    CustomerEmailAddress = table.Column<string>(name: "Customer_EmailAddress", type: "nvarchar(max)", nullable: false),
                    CustomerContactNo = table.Column<string>(name: "Customer_ContactNo", type: "nvarchar(max)", nullable: false),
                    CustomerCountry = table.Column<string>(name: "Customer_Country", type: "nvarchar(max)", nullable: false),
                    CustomerProvince = table.Column<string>(name: "Customer_Province", type: "nvarchar(max)", nullable: false),
                    CustomerTownCity = table.Column<string>(name: "Customer_TownCity", type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inquiry_inquirystatus_Status",
                        column: x => x.Status,
                        principalSchema: "marketing",
                        principalTable: "inquirystatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inquiry_inqurytype_InquiryType",
                        column: x => x.InquiryType,
                        principalSchema: "marketing",
                        principalTable: "inqurytype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "marketing",
                table: "inquirystatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Open" },
                    { 3, "Closed" }
                });

            migrationBuilder.InsertData(
                schema: "marketing",
                table: "inqurytype",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SALES INQUIRY" },
                    { 2, "LEASING INQUIRY" },
                    { 3, "BROKER ACCREDITATION" },
                    { 4, "COMMERCIAL SPACE INQUIRY" },
                    { 5, "PARKING INQUIRY" },
                    { 6, "PROPERTY MANAGEMENT" },
                    { 7, "BUSINESS PROPOSAL" },
                    { 8, "ADMIN CONCERNS" },
                    { 9, "OTHER CONCERNS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_inquiry_InquiryType",
                schema: "marketing",
                table: "inquiry",
                column: "InquiryType");

            migrationBuilder.CreateIndex(
                name: "IX_inquiry_Status",
                schema: "marketing",
                table: "inquiry",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_inquirystatus_Name",
                schema: "marketing",
                table: "inquirystatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inqurytype_Name",
                schema: "marketing",
                table: "inqurytype",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inquiry",
                schema: "marketing");

            migrationBuilder.DropTable(
                name: "inquirystatus",
                schema: "marketing");

            migrationBuilder.DropTable(
                name: "inqurytype",
                schema: "marketing");

            migrationBuilder.DropSequence(
                name: "inquiryseq",
                schema: "marketing");
        }
    }
}
