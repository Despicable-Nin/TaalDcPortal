namespace Taaldc.Sales.Api.Application.Queries.Reports
{
    public interface IReportQueries
    {
        Task<IEnumerable<OrderReportDto>> GetOrdersByDate(DateTime from, DateTime to);
        Task<IEnumerable<ReservationWithNoDPDto>> GetReservationsWithNoDP();
        Task<int> GetExpiringReservationCount();
    }
}
