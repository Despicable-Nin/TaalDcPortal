using System;
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate
{
	public sealed class CivilStatus : Enumeration
	{
		public enum CivilStatusIs
		{
            Single = 1,
            Married = 2,
            Widowed = 3,
            Divorced = 4,
            Separated = 5,
            Others = 6
        }

        public CivilStatus(int id, string name): base(id, name)
        { 
        }
	}
}

