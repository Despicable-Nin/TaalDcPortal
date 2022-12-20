using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetProperties;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, GetPropertiesResult>
{
    public Task<GetPropertiesResult> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}