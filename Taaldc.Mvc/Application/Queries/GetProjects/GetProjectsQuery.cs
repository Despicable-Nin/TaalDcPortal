using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetProjects;

public class GetProjectsQuery : IRequest<GetProjectsResult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public record GetProjectsResult
{
    public GetProjectsResult(int pageSize, int pageNumber, int totalCount, IEnumerable<ProjectDto> dtos)
    {
        Projects = dtos;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }

    public IEnumerable<ProjectDto> Projects { get; }
    public int PageSize { get; }
    public int PageNumber { get; }
    public int TotalCount { get; }
}

public record ProjectDto
{
    public ProjectDto(string name, string developer, int properties)
    {
        Name = name;
        Developer = developer;
        Properties = properties;
    }

    public string Name { get; }
    public string Developer { get; }
    public int Properties { get; }
}