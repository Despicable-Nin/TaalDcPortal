using MediatR;

namespace Taaldc.Mvc.Application.Queries.GetUnits;

public class GetUnitsQuery : IRequest<GetUnitsResult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public record GetUnitsResult
{
    public GetUnitsResult(int pageSize, int pageNumber, int totalCount, IEnumerable<UnitDto> units)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        Units = units;
    }

    public int PageSize { get; }
    public int PageNumber { get; }
    public int TotalCount { get; }

    public IEnumerable<UnitDto> Units { get; }
}

public record UnitDto
{
    public UnitDto(string property, int propertyId, string unitType, int unitTypeId, string floor, int floorId,
        double floorArea, string unitNo, decimal price, string availability)
    {
        Property = property;
        PropertyId = propertyId;
        UnitType = unitType;
        UnitTypeId = unitTypeId;
        Floor = floor;
        FloorId = floorId;
        FloorArea = floorArea;
        UnitNo = unitNo;
        Price = price;
        Availability = availability;
    }

    public string Property { get; }
    public int PropertyId { get; }
    public string UnitType { get; }
    public int UnitTypeId { get; }
    public string Floor { get; }
    public int FloorId { get; }
    public double FloorArea { get; }
    public string UnitNo { get; }
    public decimal Price { get; }
    public string Availability { get; }
}