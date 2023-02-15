namespace TaalDc.Portal.DTO.Sales
{
    public class BuyerAddressEdit_ClientDto
    {
        public int BuyerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public AddressTypeEnum Type { get; set; }
    }

    public enum AddressTypeEnum
    {
        Home,
        Business,
        Billing
    }
}
