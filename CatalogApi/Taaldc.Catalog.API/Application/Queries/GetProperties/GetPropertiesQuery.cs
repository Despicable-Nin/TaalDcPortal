using MediatR;

namespace Taaldc.Catalog.API.Application.Queries.GetProperties;

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

    public int PageSize { get; }
    public int PageNumber { get; }
    public int TotalCount { get; }


    public IEnumerable<PropertyDto> Properties { get; }
}

public record PropertyDto
{
    public PropertyDto(string name, double landArea, int towers)
    {
        Name = name;
        LandArea = landArea;
        Towers = towers;
    }

    public string Name { get; }
    public double LandArea { get; }
    public int Towers { get; }
}