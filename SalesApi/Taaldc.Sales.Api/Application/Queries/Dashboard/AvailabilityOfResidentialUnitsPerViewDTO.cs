namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record AvailabilityOfResidentialUnitsPerViewDTO
{
    public AvailabilityOfResidentialUnitsPerViewDTO(int total,
        IEnumerable<ResidentialUnitCountPerViewDTO> residentialUnitCountPerView)
    {
        Total = total;
        ResidentialUnitCountPerView = residentialUnitCountPerView;
    }

    public int Total { get; }
    public IEnumerable<ResidentialUnitCountPerViewDTO> ResidentialUnitCountPerView { get; }
}