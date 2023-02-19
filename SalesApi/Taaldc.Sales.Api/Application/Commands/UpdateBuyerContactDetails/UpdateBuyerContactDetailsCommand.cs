using MediatR;
using Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerContactDetails;

public class UpdateBuyerContactDetailsCommand : IRequest<bool>
{
    public int BuyerId { get; init; }
    public string EmailAddress { get; init; }
    public string MobileNo { get; init; }
    public string PhoneNo { get; init; }
}