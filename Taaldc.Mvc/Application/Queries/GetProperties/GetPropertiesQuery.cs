using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetProperties;

public class GetPropertiesQuery : IRequest<GetPropertiesResult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public record GetPropertiesResult
{
    public GetPropertiesResult(int pageSize, int pageNumber, int totalCount, IEnumerable<PropertyDto> properties)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        Properties = properties;
    }

    public int PageSize { get; private set; }
    public int PageNumber { get; private set; }
    public int TotalCount { get; private set; }
    

    public IEnumerable<PropertyDto> Properties { get; private set; }
}

public record PropertyDto
{
    public PropertyDto(string name, double landArea, int towers)
    {
        Name = name;
        LandArea = landArea;
        Towers = towers;
    }

    public string Name { get; private set; }
    public double LandArea { get; private set; }
    public int Towers { get; private set; }
}