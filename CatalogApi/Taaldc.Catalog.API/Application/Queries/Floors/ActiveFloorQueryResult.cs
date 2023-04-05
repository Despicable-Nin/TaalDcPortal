namespace Taaldc.Catalog.API.Application.Queries.Floors
{
    public class ActiveFloorQueryResult
    {
        public int Id { get; set; }
        public int TowerId { get; set; }
        public string FloorName { get; set; }
        public string FloorDescription { get; set; }
        public string FloorPlanFilePath { get; set; }
    }
}
