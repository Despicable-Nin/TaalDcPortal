using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage;

namespace Taaldc.Sales.Api.Application.Queries.Dashboard;

public interface IDashboardQueries
{
    Task<int> GetAvailableUnitCount();
    Task<int> GetReservedUnitCount();
    Task<int> GetSoldUnitCount();
    Task<int> GetBlockedUnitCount();

    Task<int> GetAvailableParkingCount();
    Task<int> GetReservedParkingCount();
    Task<int> GetSoldParkingCount();
    Task<int> GetBlockedParkingCount();

    Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerParkingUnitType();
    Task<IEnumerable<AvailabilityPerUnitTypeDTO>> GetAvailabilityPerCondoUnitType();
}