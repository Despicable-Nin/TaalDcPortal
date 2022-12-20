using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetTowers;

public class GetTowersHandler : IRequestHandler<GetTowersQuery, GetTowersResult>
{
    public Task<GetTowersResult> Handle(GetTowersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}