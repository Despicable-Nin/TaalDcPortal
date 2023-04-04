namespace TaalDc.Portal.DTO.Catalog.Floors
{
    public class FloorUnitAvailability_ClientDto
    {
        public string Floor { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<UnitByStatus> Units { get; set; }
    }


    public class UnitByStatus
    {
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public int UnitStatus { get; set; }
    }
}
