using System.Data;
using MediatR;
using Taaldc.Catalog.API.Application.Queries.Floors;

namespace Taaldc.Sales.API.Application.Commands.UpdateBuyerMisc;

public class UpdateBuyerMiscCommand : IRequest<bool>
{
    public int BuyerId { get; init; }
    public string Occupation { get; init; }
    public string Tin { get; init; }
    public string GovIssuedId { get; init; }
    public DateTime? GovIssuedIdValidUntil { get; init; }

}