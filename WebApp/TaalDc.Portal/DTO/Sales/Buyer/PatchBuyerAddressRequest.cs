namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class PatchBuyerAddressRequest
    {
        public int BuyerId { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public string ZipCode { get; init; }
        public AddressTypeEnum Type { get; init; }
    }

    public enum AddressTypeEnum
    {
        Home = 1,
        Business = 2,
        Billing = 3
    }
}
