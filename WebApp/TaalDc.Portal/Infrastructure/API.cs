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

        public static string GetProperties(string baseUri) => $"{baseUri}/api/adm/properties";
        public static string GetTowers(string baseUri) => $"{baseUri}/api/adm/towers";
        public static string GetFloors(string baseUri) => $"{baseUri}/api/adm/floors";
        public static string GetUnits(string baseUri) => $"{baseUri}/api/adm/units";
        public static string GetUnitTypes(string baseUri) => $"{baseUri}/api/adm/unitTypes";
    }

    public static class Sales
    {
        public static string GetUnitAndOrdersAvailability(string baseUri) => $"{baseUri}/api/v1/sales";

    }
}