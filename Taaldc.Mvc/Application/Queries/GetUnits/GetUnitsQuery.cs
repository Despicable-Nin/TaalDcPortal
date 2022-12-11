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

    public int PageSize { get; private set; }
    public int PageNumber { get; private set; }
    public int TotalCount { get; private set; }
    
    public IEnumerable<UnitDto> Units { get; private set; }
}

public record UnitDto
{
    public UnitDto(string property, int propertyId, string unitType, int unitTypeId, string floor, int floorId, double floorArea, string unitNo, decimal price, string availability)
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

    public string Property { get; private set; }
    public int PropertyId { get; private set; }
    public string UnitType { get; private set; }
    public int UnitTypeId { get; private set; }
    public string Floor { get; private set; }
    public  int FloorId { get; private set; }
    public double FloorArea { get; private set; }
    public string UnitNo { get; private set; }
    public decimal Price { get; private set; }
    public string Availability { get; private set; }
}