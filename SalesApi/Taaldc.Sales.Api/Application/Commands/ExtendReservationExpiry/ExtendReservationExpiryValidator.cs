using FluentValidation;

namespace Taaldc.Sales.Api.Application.Commands.ExtendReservationExpiry
{
    public class ExtendReservationExpiryValidator : AbstractValidator<ExtendReservationExpiryCommand>
    {
        public ExtendReservationExpiryValidator()
        {
            RuleFor(i => i.OrderId).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(i => i.Days).NotEmpty().GreaterThan(0);
        }
    }
}
