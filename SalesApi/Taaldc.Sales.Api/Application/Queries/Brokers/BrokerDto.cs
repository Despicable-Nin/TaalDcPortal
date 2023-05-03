namespace Taaldc.Sales.Api.Application.Queries.Brokers
{
    public record BrokerDto
    {
        public BrokerDto(string emailAddress)
        {
            Email = emailAddress;
        }

        public BrokerDto()
        {

        }

        public string FullName { get => FirstName + " " + LastName; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string PRCLicense { get; set; }
    }
}
