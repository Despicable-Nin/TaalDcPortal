namespace TaalDc.Portal.DTO.Catalog.Floors
{
    public class ActiveFloor_ClientDto
    {
        public int Id { get; set; }
        public int TowerId { get; set; }
        public string FloorName { get; set; }
        public string FloorDescription { get; set; }
        public string FloorPlanFilePath { get; set; }
    }
}
