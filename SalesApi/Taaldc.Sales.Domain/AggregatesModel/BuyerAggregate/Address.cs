using System;
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate
{
	public class Address : ValueObject
	{
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public AddressTypeEnum Type { get; private set; }

        public Address()
        {
        }

        public Address(AddressTypeEnum type, string street, string city, string state, string country, string zipCode)
        {
            Type = type;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }

    public enum AddressTypeEnum : int
    {
        Home = 1,
        Business = 2,
        Billing = 3
    }
}

