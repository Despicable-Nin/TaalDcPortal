namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ResidentialUnitCountPerViewDTO
{
    public ResidentialUnitCountPerViewDTO(string view, int available)
    {
        View = view;
        Available = available;
    }

    public string View { get; set; }
    public int Available { get; set; }
}