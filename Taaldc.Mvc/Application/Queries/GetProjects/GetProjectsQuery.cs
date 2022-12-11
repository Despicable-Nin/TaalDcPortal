using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetProjects;

public class GetProjectsQuery : IRequest<GetProjectsResult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public record GetProjectsResult
{
    public IEnumerable<ProjectDto> Projects { get; private set; }
    public int PageSize { get; private  set; }
    public int PageNumber { get; private set; }
    public int TotalCount { get; private set; }

    public GetProjectsResult(int pageSize,int pageNumber, int totalCount, IEnumerable<ProjectDto> dtos)
    {
        Projects = dtos;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }
}

public record ProjectDto
{
    public string Name { get; private set; }
    public string Developer { get; private set; }
    public int Properties { get; private set; }

    public ProjectDto(string name, string developer, int properties)
    {
        Name = name;
        Developer = developer;
        Properties = properties;
    }
}