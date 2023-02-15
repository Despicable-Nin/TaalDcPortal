namespace TaalDc.Portal.DTO.Sales
{
    public class BuyerIDInformationEdit_ClietnDto
    {
        public int BuyerId { get; set; }
        public string Occupation { get; private set; }
        public string TIN { get; private set; }
        public string GovIssuedID { get; private set; }
        public DateTime GovIssuedIDValidUntil { get; private set; }
    }
}
