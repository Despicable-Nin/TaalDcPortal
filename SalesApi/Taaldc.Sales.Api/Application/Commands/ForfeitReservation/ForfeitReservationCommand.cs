using MediatR;
using Taaldc.Sales.API.Application.Commands;

namespace Taaldc.Sales.Api.Application.Commands.ForfeitReservation
{
    public class ForfeitReservationCommand : IRequest<CommandResult>
    {
        public ForfeitReservationCommand(int orderId, string remarks)
        {
            OrderId = orderId;
            Remarks = remarks;
        }
        public int OrderId { get; private set; }
        public string Remarks { get; private set; }
    }
}
