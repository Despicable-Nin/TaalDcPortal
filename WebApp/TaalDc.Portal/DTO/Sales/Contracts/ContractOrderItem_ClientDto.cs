namespace TaalDc.Portal.DTO.Sales.Contracts
{
	public record ContractOrderItem_ClientDto
	{
		public int Id { get; set; }
		public int UnitId { get; set; }
		public string UnitType { get; set; }
		public string Property { get; set; }
		public string Tower { get; set; }
		public string ScenicView { get; set; }
		public string Floor { get; set; }
		public double UnitArea { get; set; }
		public double BalconyArea { get; set; }
		public double OriginalPrice { get; set; }
		public double SellingPrice { get; set; }
		public int StatusId { get; set; }
		public string Status { get; set; }
	}
}
