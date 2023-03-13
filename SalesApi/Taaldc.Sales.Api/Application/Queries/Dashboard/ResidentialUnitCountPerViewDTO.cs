namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record ResidentialUnitCountPerViewDTO
{
    public ResidentialUnitCountPerViewDTO(int viewId, string view, int available)
    {
        ViewId = viewId;
        View = view;
        Available = available;
    }

    public int ViewId { get; set; }
    public string View { get; set; }
    public int Available { get; set; }
}