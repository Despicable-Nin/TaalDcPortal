namespace TaalDc.Portal.DTO.Sales
{
    public class ParkingStats_ClientDto
    {
        public ParkingStats_ClientDto(IEnumerable<GetResidentialUnitsCountByStatusResponse> unitCountByStatus, 
            IEnumerable<GetParkingUnitTypeAvailabilityPerFloorResponse> perFloor,
            IEnumerable<GetAvailabilityPerParkingUnitTypeResponse> perUnitType)
        {
            UnitCountByStatus= unitCountByStatus;
            UnitsPerFloor= perFloor;
            UnitsPerType= perUnitType;  
        }

        public IEnumerable<GetResidentialUnitsCountByStatusResponse> UnitCountByStatus { get; set; }
        public IEnumerable<GetParkingUnitTypeAvailabilityPerFloorResponse> UnitsPerFloor { get; set; }
        public IEnumerable<GetAvailabilityPerParkingUnitTypeResponse> UnitsPerType { get; set; }
    }


    public class GetParkingUnitTypeAvailabilityPerFloorResponse
    {
        public int UnitTypeId { get; set; }
        public string UnitType { get; set; }

        public int FloorId { get; set; }
        public string Floor { get; set; }
        public int Available { get; set; }
    }

    public record GetAvailabilityPerParkingUnitTypeResponse
    {
        public int UnitTypeId { get; set; }
        public string UnitType { get; set; }
        public double FloorArea { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string UnitPriceRange { get; set; }
        public int Available { get; set; }
    }

}
