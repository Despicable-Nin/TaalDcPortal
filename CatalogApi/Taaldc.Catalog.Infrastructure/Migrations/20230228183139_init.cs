﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateSequence<int>(
                name: "floorseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence<int>(
                name: "projectseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence<int>(
                name: "propertyseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence(
                name: "scenicviewseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.CreateSequence<int>(
                name: "towerseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence<int>(
                name: "unitseq",
                schema: "catalog",
                startValue: 2000L);

            migrationBuilder.CreateSequence(
                name: "unitstatusseq",
                schema: "catalog",
                incrementBy: 10);

            migrationBuilder.CreateSequence<int>(
                name: "unittypeseq",
                schema: "catalog",
                startValue: 9L);

            migrationBuilder.CreateTable(
                name: "project",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scenicview",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scenicview", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unitstatus",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unitstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unittype",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unittype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "property",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LandArea = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_property_project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "catalog",
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tower",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tower_property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "catalog",
                        principalTable: "property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "floors",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorPlanFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TowerId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_floors_tower_TowerId",
                        column: x => x.TowerId,
                        principalSchema: "catalog",
                        principalTable: "tower",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unit",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FloorArea = table.Column<double>(type: "float", nullable: false),
                    BalconyArea = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    ScenicViewId = table.Column<int>(type: "int", nullable: false),
                    UnitStatus = table.Column<int>(type: "int", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_unit_floors_FloorId",
                        column: x => x.FloorId,
                        principalSchema: "catalog",
                        principalTable: "floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_unit_scenicview_ScenicViewId",
                        column: x => x.ScenicViewId,
                        principalSchema: "catalog",
                        principalTable: "scenicview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_unit_unitstatus_UnitStatus",
                        column: x => x.UnitStatus,
                        principalSchema: "catalog",
                        principalTable: "unitstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_unit_unittype_UnitType",
                        column: x => x.UnitType,
                        principalSchema: "catalog",
                        principalTable: "unittype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_floors_TowerId",
                schema: "catalog",
                table: "floors",
                column: "TowerId");

            migrationBuilder.CreateIndex(
                name: "IX_project_Name",
                schema: "catalog",
                table: "project",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_property_Name",
                schema: "catalog",
                table: "property",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_property_ProjectId",
                schema: "catalog",
                table: "property",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tower_Name",
                schema: "catalog",
                table: "tower",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tower_PropertyId",
                schema: "catalog",
                table: "tower",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_unit_FloorId",
                schema: "catalog",
                table: "unit",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_unit_Identifier",
                schema: "catalog",
                table: "unit",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_unit_ScenicViewId",
                schema: "catalog",
                table: "unit",
                column: "ScenicViewId");

            migrationBuilder.CreateIndex(
                name: "IX_unit_UnitStatus",
                schema: "catalog",
                table: "unit",
                column: "UnitStatus");

            migrationBuilder.CreateIndex(
                name: "IX_unit_UnitType",
                schema: "catalog",
                table: "unit",
                column: "UnitType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "unit",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "floors",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "scenicview",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "unitstatus",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "unittype",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "tower",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "property",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "project",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "floorseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "projectseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "propertyseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "scenicviewseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "towerseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "unitseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "unitstatusseq",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "unittypeseq",
                schema: "catalog");
        }
    }
}
