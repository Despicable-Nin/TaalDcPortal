using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.CreateSequence(
                name: "acquisitionseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "acquisitionstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "buyerseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "paymentseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "paymentstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "paymentypeseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "transactiontypeseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "unitreplicaseq",
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
                name: "buyer",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TownCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentstatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymenttype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymenttype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transactiontype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactiontype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unitreplica",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    TowerId = table.Column<int>(type: "int", nullable: false),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ScenicViewId = table.Column<int>(type: "int", nullable: false),
                    UnitTypeId = table.Column<int>(type: "int", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScenicView = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitArea = table.Column<double>(type: "float", nullable: false),
                    BalconyArea = table.Column<double>(type: "float", nullable: false),
                    UnitStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitStatusId = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unitreplica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acquisition",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Broker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRefundable = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId1 = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_acquisition_transactiontype_TransactionTypeId1",
                        column: x => x.TransactionTypeId1,
                        principalTable: "transactiontype",
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

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ActualPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrelationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcquisitionId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_acquisition_AcquisitionId",
                        column: x => x.AcquisitionId,
                        principalSchema: "sales",
                        principalTable: "acquisition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_payment_paymentstatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "paymentstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_paymenttype_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "paymenttype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_transactiontype_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "transactiontype",
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

            migrationBuilder.InsertData(
                table: "paymentstatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Accepted" },
                    { 2, "Pending" },
                    { 3, "Rejected" },
                    { 4, "Void" }
                });

            migrationBuilder.InsertData(
                table: "paymenttype",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Reservation" },
                    { 2, "PartialDownPayment" },
                    { 3, "FullPayment" },
                    { 4, "Penalty" }
                });

            migrationBuilder.InsertData(
                table: "transactiontype",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ForReservation" },
                    { 2, "ForAcquisition" }
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
                name: "IX_acquisition_TransactionTypeId1",
                schema: "sales",
                table: "acquisition",
                column: "TransactionTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_acquisition_UnitId",
                schema: "sales",
                table: "acquisition",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_ContactNo",
                schema: "sales",
                table: "buyer",
                column: "ContactNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_buyer_EmailAddress",
                schema: "sales",
                table: "buyer",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationNumber",
                table: "payment",
                column: "ConfirmationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_AcquisitionId",
                table: "payment",
                column: "AcquisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_PaymentTypeId",
                table: "payment",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_StatusId",
                table: "payment",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_TransactionTypeId",
                table: "payment",
                column: "TransactionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "acquisition",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "paymentstatus");

            migrationBuilder.DropTable(
                name: "paymenttype");

            migrationBuilder.DropTable(
                name: "acquisitionstatus",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "buyer",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "transactiontype");

            migrationBuilder.DropTable(
                name: "unitreplica",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "acquisitionseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "acquisitionstatusseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "buyerseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymentseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymentstatusseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymentypeseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "transactiontypeseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "unitreplicaseq",
                schema: "sales");
        }
    }
}
