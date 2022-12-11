using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetTowers;

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

     public int PageSize { get; private set; }
     public int PageNumber { get; private set; }
     public int TotalCount { get; private set; }
     
     public IEnumerable<TowerDto> Towers { get; private set; }
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

     public string Property { get; private set; }
     public string Name { get; private set; }
     public string Address { get; private set; }
     public int Units { get; private set; }
}