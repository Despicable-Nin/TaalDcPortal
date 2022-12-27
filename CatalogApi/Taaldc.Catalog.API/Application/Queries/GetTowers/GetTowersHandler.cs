using MediatR;

namespace Taaldc.Catalog.API.Application.Queries.GetTowers;

public class GetTowersHandler : IRequestHandler<GetTowersQuery, GetTowersResult>
{
    public Task<GetTowersResult> Handle(GetTowersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}