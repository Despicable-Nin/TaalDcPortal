using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added3brunittype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "THREE BEDROOM", "3BR" });

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "PENTHOUSE", "PH" });

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "RESIDENTIAL PARKING", "RP" });

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "MOTORCYCLE PARKING", "MP" });

            migrationBuilder.InsertData(
                schema: "catalog",
                table: "unittype",
                columns: new[] { "Id", "Name", "ShortCode" },
                values: new object[] { 8, "COMMERCIAL SPACE", "CS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "PENTHOUSE", "PH" });

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "RESIDENTIAL PARKING", "RP" });

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "MOTORCYCLE PARKING", "MP" });

            migrationBuilder.UpdateData(
                schema: "catalog",
                table: "unittype",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "ShortCode" },
                values: new object[] { "COMMERCIAL SPACE", "CS" });
        }
    }
}
