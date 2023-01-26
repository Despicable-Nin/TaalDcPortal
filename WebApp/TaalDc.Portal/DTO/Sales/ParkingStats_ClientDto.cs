namespace TaalDc.Portal.DTO.Sales
{
    public class ParkingStats_ClientDto
    {
        public ParkingStats_ClientDto(IEnumerable<UnitCountByStatus_ClientDto> unitCountByStatus, 
            IEnumerable<ParkingUnitAvailabilityPerFloor_ClientDto> perFloor,
            IEnumerable<ParkingUnitAvailabilityPerUnitType_ClientDto> perUnitType)
        {
            UnitCountByStatus= unitCountByStatus;
            UnitsPerFloor= perFloor;
            UnitsPerType= perUnitType;  
        }

        public IEnumerable<UnitCountByStatus_ClientDto> UnitCountByStatus { get; set; }
        public IEnumerable<ParkingUnitAvailabilityPerFloor_ClientDto> UnitsPerFloor { get; set; }
        public IEnumerable<ParkingUnitAvailabilityPerUnitType_ClientDto> UnitsPerType { get; set; }
    }


    public class ParkingUnitAvailabilityPerFloor_ClientDto
    {
        public string UnitType { get; set; }
        public string Floor { get; set; }
        public int Available { get; set; }
    }

    public record ParkingUnitAvailabilityPerUnitType_ClientDto
    {
        public string UnitType { get; set; }
        public double FloorArea { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string UnitPriceRange { get; set; }
        public int Available { get; set; }
    }

}
