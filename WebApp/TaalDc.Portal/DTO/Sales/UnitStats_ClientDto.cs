using System.Text.Json.Serialization;

namespace TaalDc.Portal.DTO.Sales
{
    public class GetResidentialUnitsCountByStatusResponse
    {
        public string UnitStatus { get; set; }
        public int Count { get; set; }
    }

    public class GetResidentialAvailabilityByTypeResponse
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


    public class GetResidentialAvailabilityByViewResponse
    {
        public string View { get; set; }
        public int Available { get; set; }
    }



    public class UnitStats_ClientDto
    {
        public UnitStats_ClientDto(IEnumerable<GetResidentialUnitsCountByStatusResponse> unitCountByStatus, IEnumerable<GetResidentialAvailabilityByTypeResponse> availabilityByUnitType, IEnumerable<GetResidentialAvailabilityByViewResponse> availabilityByViews)
        {
            UnitCountByStatus = unitCountByStatus;
            AvailabilityByUnitType = availabilityByUnitType;
            AvailabilityByViews = availabilityByViews;
        }

        public IEnumerable<GetResidentialUnitsCountByStatusResponse> UnitCountByStatus { get; set; }
        public IEnumerable<GetResidentialAvailabilityByTypeResponse> AvailabilityByUnitType { get; set; }
        public IEnumerable<GetResidentialAvailabilityByViewResponse> AvailabilityByViews { get; set; }
    }
}
