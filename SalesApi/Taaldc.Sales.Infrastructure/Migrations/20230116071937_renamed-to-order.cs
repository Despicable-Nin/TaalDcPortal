using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamedtoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_acquisition_AcquisitionId",
                table: "payment");

            migrationBuilder.DropTable(
                name: "acquisition",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "acquisitionstatus",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "acquisitionseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "acquisitionstatusseq",
                schema: "sales");

            migrationBuilder.RenameColumn(
                name: "AcquisitionId",
                table: "payment",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_payment_AcquisitionId",
                table: "payment",
                newName: "IX_payment_OrderId");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "orderstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "orderstatus",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Broker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsRefundable = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "sales",
                        principalTable: "buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_orderstatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "sales",
                        principalTable: "orderstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_unitreplica_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "sales",
                        principalTable: "unitreplica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "sales",
                table: "orderstatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "FullyPaid" },
                    { 2, "PartiallyPaid" },
                    { 3, "Cancelled" },
                    { 4, "Reserved" },
                    { 5, "New" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_BuyerId",
                schema: "sales",
                table: "order",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_order_StatusId",
                schema: "sales",
                table: "order",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_order_UnitId",
                schema: "sales",
                table: "order",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_order_OrderId",
                table: "payment",
                column: "OrderId",
                principalSchema: "sales",
                principalTable: "order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_order_OrderId",
                table: "payment");

            migrationBuilder.DropTable(
                name: "order",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "orderstatus",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "orderstatusseq",
                schema: "sales");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "payment",
                newName: "AcquisitionId");

            migrationBuilder.RenameIndex(
                name: "IX_payment_OrderId",
                table: "payment",
                newName: "IX_payment_AcquisitionId");

            migrationBuilder.CreateSequence(
                name: "acquisitionseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "acquisitionstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "acquisitionstatus",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acquisitionstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acquisition",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Broker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRefundable = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acquisition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acquisition_acquisitionstatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "sales",
                        principalTable: "acquisitionstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acquisition_buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "sales",
                        principalTable: "buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acquisition_unitreplica_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "sales",
                        principalTable: "unitreplica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "sales",
                table: "acquisitionstatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "FullyPaid" },
                    { 2, "PartiallyPaid" },
                    { 3, "Cancelled" },
                    { 4, "Reserved" },
                    { 5, "New" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_acquisition_BuyerId",
                schema: "sales",
                table: "acquisition",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_acquisition_StatusId",
                schema: "sales",
                table: "acquisition",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_acquisition_UnitId",
                schema: "sales",
                table: "acquisition",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_acquisition_AcquisitionId",
                table: "payment",
                column: "AcquisitionId",
                principalSchema: "sales",
                principalTable: "acquisition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
