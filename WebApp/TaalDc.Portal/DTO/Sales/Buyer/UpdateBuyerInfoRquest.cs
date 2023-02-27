namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class UpdateBuyerInfoRquest
    {
        public int BuyerId { get; init; }
        public string Salutation { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public DateTime DoB { get; init; }
        public int CivilStatusId { get; init; }
        public bool IsCorporate { get; init; }
    }
}
