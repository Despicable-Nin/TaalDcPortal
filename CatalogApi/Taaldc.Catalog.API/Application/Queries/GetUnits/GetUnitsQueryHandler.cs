using MediatR;

namespace Taaldc.Catalog.API.Application.Queries.GetUnits;

public class GetUnitsQueryHandler : IRequestHandler<GetUnitsQuery, GetUnitsResult>
{
    public Task<GetUnitsResult> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}