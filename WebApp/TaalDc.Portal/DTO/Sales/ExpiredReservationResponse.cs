namespace TaalDc.Portal.DTO.Sales
{
    public class ExpiredReservationResponse
    {
        public int Id { get; init; }
        public int BuyerId { get; init; }
        public string FullName { get; init; }
        public string EmailAddress { get; init; }
        public DateTime ReservationExpiresOn { get; init; }
        public int NoOfDays { get; init; }
        public string Property { get; init; }
        public string Tower { get; init; }
        public string Floor { get; init; }
        public int UnitId { get; init; }
        public string UnitType { get; init; }
        public string Unit { get; init; }
    }
}
