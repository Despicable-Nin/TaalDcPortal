namespace Taaldc.Sales.Api.Application.Queries.Reports
{
    public class OrderReportDto
    {
        public int No { get; set; }
        public string UnitNumber { get; set; }
        public string NameOfClient { get; set; }
        public string CorporateClientName { get; set; }
        public string NameOfBroker { get; set; }
        public string PRCLicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string UnitType { get; set; }
        public double TotalUnitArea { get; set; }
        public string Orientation { get; set; }
        public double UnitPriceExclusiveOfVAT { get; set; }
        public string UnitFloor { get; set; }
        public double ReservationFeeAmount { get; set; }
        public string ARNo { get; set; }
        public DateTime DateOfReservation { get; set; }
        public string PaymentMode { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}
