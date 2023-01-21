namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ResidentialUnitCountPerViewDTO
{
    public ResidentialUnitCountPerViewDTO(string view, int count)
    {
        View = view;
        Count = count;
    }

    public string View { get; private set; }
    public int Count { get; private set; }
}