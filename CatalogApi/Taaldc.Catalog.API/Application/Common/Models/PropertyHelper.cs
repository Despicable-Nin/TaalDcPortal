namespace Taaldc.Catalog.API.Application.Common.Models
{
    public static class PropertyHelper
    {
        public static string GetPropertySorterName(string sorter, SortOrderEnum sortOrder = SortOrderEnum.ASC)
        {
            var sortByString = $"p.Id {sortOrder}";

            if (!string.IsNullOrEmpty(sorter))
            {
                switch (sorter)
                {
                    case "property_id":
                        sortByString = $"p.Id {sortOrder}";
                        break;
                    case "property_name":
                        sortByString = $"p.Name {sortOrder}";
                        break;
                    case "land_area":
                        sortByString = $"p.LandArea {sortOrder}";
                        break;
                    case "towers":
                        sortByString = $"COUNT(t.Id) {sortOrder}";
                        break;
                    default:
                        sortByString = $"p.Id ASC";
                        break;
                }
            }

            return sortByString;
        }
    }
}
