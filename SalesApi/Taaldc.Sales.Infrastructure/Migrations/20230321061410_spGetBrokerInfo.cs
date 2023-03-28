using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class spGetBrokerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sp = @"USE taaldb_sales
					GO

					CREATE PROCEDURE [sales].[spGetBrokerInfo]
						@EmailAddress VARCHAR(50)
					AS

					BEGIN
	
						/*TEST 
						DECLARE @EmailAddress VARCHAR(50) = 'brokeruser@gmail.com';
						*/

						SELECT 
							p.FirstName,
							p.LastName,
							u.Email,
							p.Company,
							p.PRCLicense
						FROM taaldb_identity.dbo.UserProfiles p
						JOIN taaldb_identity.dbo.AspNetUsers u
						ON p.[Identity] = u.[Id]
						WHERE u.Email = @EmailAddress
					END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
