using FluentValidation;

namespace Taaldc.Sales.Api.Application.Commands.ForfeitReservation
{
    public class ForfeitReservationCommandValidator  : AbstractValidator<ForfeitReservationCommand>
    {
        public ForfeitReservationCommandValidator()
        {
            RuleFor(i => i.Remarks).NotEmpty();
            RuleFor(i => i.OrderId).NotEmpty();
        }
    }
}
