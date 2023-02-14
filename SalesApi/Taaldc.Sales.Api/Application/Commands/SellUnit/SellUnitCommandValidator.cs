using FluentValidation;

namespace Taaldc.Sales.Api.Application.Commands.SellUnit;

public class SellUnitCommandValidator : AbstractValidator<SellUnitCommand>
{
    public SellUnitCommandValidator()
    {
        RuleFor(i => i.Broker).NotEmpty();
        RuleFor(i => i.PaymentMethod).NotEmpty();
        RuleFor(i => i.BuyerId).NotEmpty();
        RuleFor(i => i.PaymentReferenceId).NotEmpty();
        RuleFor(i => i.ReservationFee).NotEmpty().GreaterThan(0M);
        RuleFor(i => i.ReservationConfirmation).NotEmpty();
        RuleFor(i => i.DownpaymentConfirmation).NotEmpty().When(i => i.Downpayment > 0M);

        RuleFor(i => i.OrderItems).NotEmpty();
        RuleFor(i => i.OrderItems.Select(a => a.UnitId).Distinct().Count().Equals(i.OrderItems.Count()));
    }
}

public class OrderItemValidator : AbstractValidator<OrderItemDTO>
{
    public OrderItemValidator()
    {
        RuleFor(i => i.UnitId).NotEmpty();
        RuleFor(i => i.Price).NotEmpty();
    }
}