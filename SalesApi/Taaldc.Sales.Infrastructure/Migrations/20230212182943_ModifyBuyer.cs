using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyBuyer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                schema: "sales",
                table: "buyer",
                newName: "TIN");

            migrationBuilder.RenameColumn(
                name: "TownCity",
                schema: "sales",
                table: "buyer",
                newName: "Occupation");

            migrationBuilder.RenameColumn(
                name: "Province",
                schema: "sales",
                table: "buyer",
                newName: "MobileNo");

            migrationBuilder.RenameColumn(
                name: "Country",
                schema: "sales",
                table: "buyer",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "ContactNo",
                schema: "sales",
                table: "buyer",
                newName: "PhoneNo");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "sales",
                table: "buyer",
                newName: "GovIssuedID");

            migrationBuilder.RenameIndex(
                name: "IX_buyer_ContactNo",
                schema: "sales",
                table: "buyer",
                newName: "IX_buyer_PhoneNo");

            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                schema: "sales",
                table: "buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinessAddressId",
                schema: "sales",
                table: "buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CivilStatusId",
                schema: "sales",
                table: "buyer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                schema: "sales",
                table: "buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DoB",
                schema: "sales",
                table: "buyer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GovIssuedIDValidUntil",
                schema: "sales",
                table: "buyer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HomeAddressId",
                schema: "sales",
                table: "buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorporate",
                schema: "sales",
                table: "buyer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpouseId",
                schema: "sales",
                table: "buyer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "address",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "civilStatus",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_civilStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SECRegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    President = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorpSec = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "sales",
                table: "civilStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Single" },
                    { 2, "Married" },
                    { 3, "Widowed" },
                    { 4, "Divorced" },
                    { 5, "Separated" },
                    { 6, "Others" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_buyer_BillingAddressId",
                schema: "sales",
                table: "buyer",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_BusinessAddressId",
                schema: "sales",
                table: "buyer",
                column: "BusinessAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_CivilStatusId",
                schema: "sales",
                table: "buyer",
                column: "CivilStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_CompanyId",
                schema: "sales",
                table: "buyer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_HomeAddressId",
                schema: "sales",
                table: "buyer",
                column: "HomeAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_buyer_address_BillingAddressId",
                schema: "sales",
                table: "buyer",
                column: "BillingAddressId",
                principalSchema: "sales",
                principalTable: "address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_buyer_address_BusinessAddressId",
                schema: "sales",
                table: "buyer",
                column: "BusinessAddressId",
                principalSchema: "sales",
                principalTable: "address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_buyer_address_HomeAddressId",
                schema: "sales",
                table: "buyer",
                column: "HomeAddressId",
                principalSchema: "sales",
                principalTable: "address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_buyer_civilStatus_CivilStatusId",
                schema: "sales",
                table: "buyer",
                column: "CivilStatusId",
                principalSchema: "sales",
                principalTable: "civilStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_buyer_company_CompanyId",
                schema: "sales",
                table: "buyer",
                column: "CompanyId",
                principalSchema: "sales",
                principalTable: "company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_buyer_address_BillingAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_buyer_address_BusinessAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_buyer_address_HomeAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_buyer_civilStatus_CivilStatusId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_buyer_company_CompanyId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropTable(
                name: "address",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "civilStatus",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "company",
                schema: "sales");

            migrationBuilder.DropIndex(
                name: "IX_buyer_BillingAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropIndex(
                name: "IX_buyer_BusinessAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropIndex(
                name: "IX_buyer_CivilStatusId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropIndex(
                name: "IX_buyer_CompanyId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropIndex(
                name: "IX_buyer_HomeAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "BusinessAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "CivilStatusId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "DoB",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "GovIssuedIDValidUntil",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "HomeAddressId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "IsCorporate",
                schema: "sales",
                table: "buyer");

            migrationBuilder.DropColumn(
                name: "SpouseId",
                schema: "sales",
                table: "buyer");

            migrationBuilder.RenameColumn(
                name: "TIN",
                schema: "sales",
                table: "buyer",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                schema: "sales",
                table: "buyer",
                newName: "ContactNo");

            migrationBuilder.RenameColumn(
                name: "Occupation",
                schema: "sales",
                table: "buyer",
                newName: "TownCity");

            migrationBuilder.RenameColumn(
                name: "MobileNo",
                schema: "sales",
                table: "buyer",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                schema: "sales",
                table: "buyer",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "GovIssuedID",
                schema: "sales",
                table: "buyer",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_buyer_PhoneNo",
                schema: "sales",
                table: "buyer",
                newName: "IX_buyer_ContactNo");
        }
    }
}
