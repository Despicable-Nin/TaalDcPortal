using System.Drawing.Printing;

namespace TaalDc.Portal.Infrastructure;

public static class API
{
    public static class Marketing
    {
        public static string GetInquiries(string baseUri, int pageSize, int pageNumber) => $"{baseUri}/api/mkt/inquiries?pageSize={pageSize}&pageNumber={pageNumber}";
        public static string PostInquiry(string baseUri) => $"{baseUri}/api/mkt/inquiries";
    }

    public static class Catalog
    {
        public static string AddFloor(string baseUri) => $"{baseUri}/api/adm/floors";
        public static string AddProperty(string baseUri) => $"{baseUri}/api/adm/properties";
        public static string AddTower(string baseUri) => $"{baseUri}/api/adm/towers";
        public static string AddUnit(string baseUri) => $"{baseUri}/api/adm/units";
        public static string AddUnitType(string baseUri) => $"{baseUri}/api/adm/unitTypes";
        public static string UpdateUnitStatus(string baseUri) => $"{baseUri}/api/adm/units/change-status";
        
        public static string EditUnit(string baseUri,int id) => $"{baseUri}/api/adm/units";



        public static string GetProperties(string baseUri) => $"{baseUri}/api/adm/properties";
        public static string GetTowers(string baseUri) => $"{baseUri}/api/adm/towers";
        public static string GetFloors(string baseUri) => $"{baseUri}/api/adm/floors";
        public static string GetUnits(string baseUri) => $"{baseUri}/api/adm/units";
        public static string GetUnitTypes(string baseUri) => $"{baseUri}/api/adm/unitTypes";

        public static string GetFloorById(string baserUri,int id) => $"{baserUri}/api/adm/floors/{id}";
        public static string GetUnitById(string baseUri, int id) => $"{baseUri}/api/adm/units/{id}";
    }

    public static class Sales
    {
        public static string GetSales(string baseUri) => $"{baseUri}/api/v1/sales";
		public static string SellUnit(string baseUrl) => $"{baseUrl}/api/v1/sales";
        public static string AcceptPayment(string baseUrl, int orderId, int paymentId) => $"{baseUrl}/api/v1/sales/{orderId}/payments/{paymentId}/approve";
        public static string AddPayment(string baseUrl, int orderId) => $"{baseUrl}/api/v1/sales/{orderId}/payments";
        public static string GetAvailableUnitCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/residential/available";
        public static string GetReservedUnitCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/residential/reserved";
        public static string GetSoldUnitCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/residential/sold";
        public static string GetBlockedUnitCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/residential/blocked";
        public static string GetAvailabilityOfResidentialUnitPerView(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/residential/available-per-view";
        public static string GetAvailabilityPerResidentialUnitType(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/residential/available-per-unit-type";
        public static string GetAvailableParkingCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/parking/available";
        public static string GetReservedParkingCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/parking/reserved";
        public static string GetBlockedParkingCount(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/parking/blocked";
        public static string GetParkingUnitTypeAvailabilityPerFloor(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/parking/available-per-floor";
        public static string GetAvailabilityPerParkingUnitType(string baseUrl) =>
            $"{baseUrl}/api/v1/dashboard/parking/available-per-unit-type";

	}
}