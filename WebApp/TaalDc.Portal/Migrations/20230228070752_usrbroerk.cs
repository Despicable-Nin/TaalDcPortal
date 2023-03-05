using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalDc.Portal.Migrations
{
    /// <inheritdoc />
    public partial class usrbroerk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PRCLicense",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PRCLicense",
                table: "UserProfiles");
        }
    }
}
