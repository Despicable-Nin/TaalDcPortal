namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public record AvailabilityOfResidentialUnitsPerViewDTO
{
    public AvailabilityOfResidentialUnitsPerViewDTO(int total, IEnumerable<ResidentialUnitCountPerViewDTO> residentialUnitCountPerView)
    {
        Total = total;
        ResidentialUnitCountPerView = residentialUnitCountPerView;
    }

    public int Total { get; private set; }
    public IEnumerable<ResidentialUnitCountPerViewDTO> ResidentialUnitCountPerView { get; private set; }
}