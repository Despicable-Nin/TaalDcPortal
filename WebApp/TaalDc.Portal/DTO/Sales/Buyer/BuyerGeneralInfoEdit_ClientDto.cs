namespace TaalDc.Portal.DTO.Sales.Buyer
{
    public class BuyerGeneralInfoEdit_ClientDto
    {
        public int BuyerId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public int CivilStatusId { get; set; }
        public bool IsCorporate { get; set; }
    }
}
