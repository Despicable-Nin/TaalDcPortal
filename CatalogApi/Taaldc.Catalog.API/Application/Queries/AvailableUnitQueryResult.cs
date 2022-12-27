namespace Taaldc.Catalog.API.Application.Queries;

public record AvailableUnitQueryResult
{
    public AvailableUnitQueryResult(int pageSize, int pageNumber, int pageCount, IEnumerable<AvailableUnit> units)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        PageCount = pageCount;
        Units = units;
    }

    public int PageSize { get; }
    public int PageNumber { get; }
    public int PageCount { get; }
    public IEnumerable<AvailableUnit> Units { get; }
}