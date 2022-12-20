using MediatR;

namespace Taaldc.Catalog.API.Application.Queries.GetProjects;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, GetProjectsResult>
{
    public Task<GetProjectsResult> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}