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
                name: "addressseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "buyerseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "civilstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "companyseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "orderitemseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence<int>(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.CreateSequence(
                name: "orderstatusseq",
                schema: "sales",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "paymentoptionseq",
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
                name: "paymenttypeseq",
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
                name: "civilStatus",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_civilStatus", x => x.Id);
                });

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
                name: "paymentoption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentoption", x => x.Id);
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
                    UnitTypeShortCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "buyer",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CivilStatusId = table.Column<int>(type: "int", nullable: false),
                    IsCorporate = table.Column<bool>(type: "bit", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GovIssuedId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GovIssuedIdValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerId = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_buyer_civilStatus_CivilStatusId",
                        column: x => x.CivilStatusId,
                        principalSchema: "sales",
                        principalTable: "civilStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "address",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_address_buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "sales",
                        principalTable: "buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "company",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    CorpSec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "sales",
                        principalTable: "buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0.0m),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Broker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "orderitem",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0.0m),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderitem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderitem_order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sales",
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderitem_unitreplica_UnitId",
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
                    ConfirmationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_payment_order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sales",
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_address_BuyerId",
                schema: "sales",
                table: "address",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_CivilStatusId",
                schema: "sales",
                table: "buyer",
                column: "CivilStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_EmailAddress",
                schema: "sales",
                table: "buyer",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_buyer_FirstName_MiddleName_LastName",
                schema: "sales",
                table: "buyer",
                columns: new[] { "FirstName", "MiddleName", "LastName" },
                unique: true,
                filter: "[MiddleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_PhoneNo",
                schema: "sales",
                table: "buyer",
                column: "PhoneNo");

            migrationBuilder.CreateIndex(
                name: "IX_company_BuyerId",
                schema: "sales",
                table: "company",
                column: "BuyerId",
                unique: true);

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
                name: "IX_orderitem_OrderId",
                schema: "sales",
                table: "orderitem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitem_UnitId",
                schema: "sales",
                table: "orderitem",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_OrderId",
                table: "payment",
                column: "OrderId");

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
                name: "address",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "company",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "orderitem",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "paymentoption");

            migrationBuilder.DropTable(
                name: "unitreplica",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "order",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "paymentstatus");

            migrationBuilder.DropTable(
                name: "paymenttype");

            migrationBuilder.DropTable(
                name: "transactiontype");

            migrationBuilder.DropTable(
                name: "buyer",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "orderstatus",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "civilStatus",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "addressseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "buyerseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "civilstatusseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "companyseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "orderitemseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "orderstatusseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymentoptionseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymentseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymentstatusseq",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "paymenttypeseq",
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
