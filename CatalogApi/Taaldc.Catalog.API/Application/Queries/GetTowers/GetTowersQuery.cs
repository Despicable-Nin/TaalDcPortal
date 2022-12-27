using MediatR;

namespace Taaldc.Catalog.API.Application.Queries.GetTowers;

public class GetTowersQuery : IRequest<GetTowersResult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public record GetTowersResult
{
    public GetTowersResult(int pageSize, int pageNumber, int totalCount, IEnumerable<TowerDto> towers)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        Towers = towers;
    }

    public int PageSize { get; }
    public int PageNumber { get; }
    public int TotalCount { get; }

    public IEnumerable<TowerDto> Towers { get; }
}

public record TowerDto
{
    public TowerDto(string property, string name, string address, int units)
    {
        Property = property;
        Name = name;
        Address = address;
        Units = units;
    }

    public string Property { get; }
    public string Name { get; }
    public string Address { get; }
    public int Units { get; }
}