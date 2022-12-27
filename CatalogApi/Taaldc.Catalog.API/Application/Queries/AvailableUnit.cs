namespace Taaldc.Catalog.API.Application.Queries;

public record AvailableUnit
{
    public int Id { get;  }
    public string Identifier { get;  }
    public decimal Price { get; }
    public double FloorArea { get; }
    public string Floor { get; }
    public string FloorDesc { get; }
    public string View { get; }
    public string Status { get; }
    public string Type { get; }
    public string TypeCode { get; }
}