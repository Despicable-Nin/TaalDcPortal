namespace TaalDc.Portal.Infrastructure;

public static class API
{
    public static class Marketing
    {
        public static string GetInquiries(string baseUri, int pageSize, int pageNumber)
        {
            return $"{baseUri}/api/mkt/inquiries?pageSize={pageSize}&pageNumber={pageNumber}";
        }

        public static string GetInquiry(string baseUri, int id)
        {
            return $"{baseUri}/api/mkt/inquiries/{id}";
        }


        public static string PostInquiry(string baseUri)
        {
            return $"{baseUri}/api/mkt/inquiries";
        }
    }

    public static class Catalog
    {
        public static string AddFloor(string baseUri)
        {
            return $"{baseUri}/api/adm/floors";
        }

        public static string AddProperty(string baseUri)
        {
            return $"{baseUri}/api/adm/properties";
        }

        public static string AddTower(string baseUri)
        {
            return $"{baseUri}/api/adm/towers";
        }

        public static string AddUnit(string baseUri)
        {
            return $"{baseUri}/api/adm/units";
        }

        public static string AddUnitType(string baseUri)
        {
            return $"{baseUri}/api/adm/unitTypes";
        }

        public static string UpdateUnitStatus(string baseUri)
        {
            return $"{baseUri}/api/adm/units/change-status";
        }

        public static string EditUnit(string baseUri, int id)
        {
            return $"{baseUri}/api/adm/units";
        }


        public static string GetProperties(string baseUri)
        {
            return $"{baseUri}/api/adm/properties";
        }

        public static string GetTowers(string baseUri)
        {
            return $"{baseUri}/api/adm/towers";
        }

        public static string GetFloors(string baseUri)
        {
            return $"{baseUri}/api/adm/floors";
        }

        public static string GetUnits(string baseUri)
        {
            return $"{baseUri}/api/adm/units";
        }

        public static string GetUnitTypes(string baseUri)
        {
            return $"{baseUri}/api/adm/unitTypes";
        }

        public static string GetFloorById(string baserUri, int id)
        {
            return $"{baserUri}/api/adm/floors/{id}";
        }

        public static string GetUnitById(string baseUri, int id)
        {
            return $"{baseUri}/api/adm/units/{id}";
        }
    }

    public static class Sales
    {
        public static string GetSales(string baseUri)
        {
            return $"{baseUri}/api/v1/sales";
        }

        public static string SellUnit(string baseUrl)
        {
            return $"{baseUrl}/api/v1/sales";
        }

        public static string AcceptPayment(string baseUrl, int orderId, int paymentId)
        {
            return $"{baseUrl}/api/v1/sales/{orderId}/payments/{paymentId}/approve";
        }

        public static string VoidPayment(string baseUrl, int orderId, int paymentId)
        {
            return $"{baseUrl}/api/v1/sales/{orderId}/payments/{paymentId}/void";
        }

        public static string AddPayment(string baseUrl, int orderId)
        {
            return $"{baseUrl}/api/v1/sales/{orderId}/payments";
        }


        public static string GetUnitCountSummaryByStatus(string baseUri)
        {
            return $"{baseUri}/api/v1/dashboard/residential/status-summary";
        }


        public static string GetAvailableUnitCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/residential/available";
        }

        public static string GetReservedUnitCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/residential/reserved";
        }

        public static string GetSoldUnitCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/residential/sold";
        }

        public static string GetBlockedUnitCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/residential/blocked";
        }

        public static string GetAvailabilityOfResidentialUnitPerView(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/residential/available-per-view";
        }

        public static string GetAvailabilityPerResidentialUnitType(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/residential/available-per-unit-type";
        }

        public static string GetParkingCountSummaryByStatus(string baseUri)
        {
            return $"{baseUri}/api/v1/dashboard/parking/status-summary";
        }


        public static string GetAvailableParkingCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/parking/available";
        }

        public static string GetReservedParkingCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/parking/reserved";
        }

        public static string GetBlockedParkingCount(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/parking/blocked";
        }

        public static string GetParkingUnitTypeAvailabilityPerFloor(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/parking/available-per-floor";
        }

        public static string GetAvailabilityPerParkingUnitType(string baseUrl)
        {
            return $"{baseUrl}/api/v1/dashboard/parking/available-per-unit-type";
        }

        #region Buyer
        public static string AddBuyer(string baseUrl)
        {
            return $"{baseUrl}/api/v1/buyers";
        }

        #endregion
    }
}