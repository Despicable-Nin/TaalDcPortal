namespace TaalDc.Portal.DTO.Sales.Contracts
{
	public record Contract_ClientDto
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public int BuyerId { get; set; }
		public DateTime TransactionDate { get; set; }
		public string Broker { get; set; }
		public string PaymentMethod { get; set; }
		public string Remarks { get; set; }

		public IEnumerable<ContractOrderItem_ClientDto> OrderItems { get; set; }

	}
}
