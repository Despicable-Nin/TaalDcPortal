using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterspGetOrdersByDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE [taaldb_sales]
						GO
						
						SET ANSI_NULLS ON
						GO
						
						SET QUOTED_IDENTIFIER ON
						GO
						
						ALTER PROCEDURE [sales].[spGetOrdersByDate]
										@DateFrom DATE,
										@DateTo DATE
						AS 

						BEGIN
							SET NOCOUNT ON;

							SELECT o.Id AS [No], 
							ur.Unit AS [UnitNumber], 
							b.FirstName + ' ' + b.LastName AS [NameOfClient], 
							IIF(b.IsCorporate = 1, c.Name, '') AS [CorporateClientName],
							o.Broker_Name AS [NameOfBroker],
							usrprof.PRCLicense AS [PRCLicenseNumber],
							b.PhoneNo AS [PhoneNumber],
							b.MobileNo AS [MobileNumber],
							ur.UnitType,
							ur.UnitArea + ur.BalconyArea AS [TotalUnitArea],
							ur.ScenicView AS [Orientation],
							oi.Price AS [UnitPriceExclusiveOfVAT],
							ur.Floor AS [UnitFloor],
							res.AmountPaid AS [ReservationFeeAmount],
							res.ConfirmationNumber AS [ARNo],
							res.ActualPaymentDate AS [DateOfReservation],
							res.PaymentMethod AS [PaymentMode],
							ISNULL(res.Remarks,'') AS [Remarks],
							ur.UnitStatus AS [Status]
							FROM [sales].[order] o
							JOIN [sales].[buyer] b
							ON o.BuyerId = b.Id
							LEFT JOIN [sales].[company] c
							ON c.BuyerId = b.Id
							JOIN [sales].[orderstatus] os
							ON o.StatusId = os.Id
							LEFT JOIN [sales].[orderitem] oi
							ON o.Id = oi.OrderId
							JOIN [sales].[unitreplica] ur
							ON oi.UnitId = ur.Id
							LEFT JOIN [dbo].[payment] res
							ON res.OrderId = o.Id AND 
							res.PaymentTypeId = 1

							LEFT JOIN taaldb_identity.dbo.AspNetUsers aspu
							ON aspu.Email = o.[Broker_Email]
							LEFT JOIN taaldb_identity.dbo.UserProfiles usrprof
							ON aspu.Id = usrprof.[Identity]

							WHERE res.ActualPaymentDate BETWEEN @DateFrom AND @DateTo
						END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
