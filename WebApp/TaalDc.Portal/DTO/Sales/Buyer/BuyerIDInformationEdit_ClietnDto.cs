namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class BuyerIDInformationEdit_ClietnDto
    {
        public int BuyerId { get; set; }
        public string Occupation { get; set; }
        public string TIN { get; set; }
        public string GovIssuedID { get; set; }
        public DateTime GovIssuedIDValidUntil { get; set; }
    }
}
