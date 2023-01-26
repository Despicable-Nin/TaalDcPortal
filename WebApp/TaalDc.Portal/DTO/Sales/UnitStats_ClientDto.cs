using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Sales
{
    public class UnitCountByStatus_ClientDto
    {
        public string UnitStatus { get; set; }
        public int Count { get; set; }
    }

    public class AvailabilityByUnitType_ClientDto
    {
        public string UnitTypeShortCode { get; set; }
        public double MinArea { get; set; }
        public double MaxArea { get; set; }
        public string FloorArea { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string UnitPriceRange { get; set; }
        public int Available { get; set; }
    }


    public class AvailabilityByView_ClientDto
    {
        public string View { get; set; }
        public int Available { get; set; }
    }



    public class UnitStats_ClientDto
    {
        public UnitStats_ClientDto(IEnumerable<UnitCountByStatus_ClientDto> unitCountByStatus, IEnumerable<AvailabilityByUnitType_ClientDto> availabilityByUnitType, IEnumerable<AvailabilityByView_ClientDto> availabilityByViews)
        {
            UnitCountByStatus = unitCountByStatus;
            AvailabilityByUnitType = availabilityByUnitType;
            AvailabilityByViews = availabilityByViews;
        }

        public IEnumerable<UnitCountByStatus_ClientDto> UnitCountByStatus { get; set; }
        public IEnumerable<AvailabilityByUnitType_ClientDto> AvailabilityByUnitType { get; set; }
        public IEnumerable<AvailabilityByView_ClientDto> AvailabilityByViews { get; set; }
    }
}
