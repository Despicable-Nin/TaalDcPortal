namespace Taaldc.Sales.Api.Application.Queries.Reports
{
    public record OrderReportDto
    {
        public int No { get; init; }
        public string UnitNumber { get; init; }
        public string NameOfClient { get; init; }
        public string CorporateClientName { get; init; }
        public string NameOfBroker { get; init; }
        public string PRCLicenseNumber { get; init; }
        public string PhoneNumber { get; init; }
        public string MobileNumber { get; init; }
        public string UnitType { get; init; }
        public double TotalUnitArea { get; init; }
        public string Orientation { get; init; }
        public double UnitPriceExclusiveOfVAT { get; init; }
        public string UnitFloor { get; init; }
        public double ReservationFeeAmount { get; init; }
        public string ARNo { get; init; }
        public DateTime DateOfReservation { get; init; }
        public string PaymentMode { get; init; }
        public string Remarks { get; init; }
        public string Status { get; init; }
    }

    public record ReservationWithNoDPDto
    {
        public int Id { get; init; }
        public int BuyerId { get; init; }
        public string FullName { get; init; }
        public string EmailAddress { get; init; }
        public DateTime ReservationExpiresOn { get; init; }
        public int NoOfDays { get; init; }
        public string Property { get; init; }
        public string Tower { get; init; }
        public string Floor { get; init; }
        public int UnitId { get; init; }
        public string UnitType { get; init; }
        public string Unit { get; init; }
        
    }
}
