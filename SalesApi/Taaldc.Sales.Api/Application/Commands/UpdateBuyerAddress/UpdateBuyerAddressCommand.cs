using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerAddress;

public class UpdateBuyerAddressCommand : IRequest<bool>
{
    public int BuyerId { get; init; }
    public string Street { get; init; }
    public string City { get; init;}
    public string State { get; init;}
    public string Country { get; init; }
    public string ZipCode { get; init; }
    public AddressTypeEnum Type { get; private set; }

}