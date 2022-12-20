using FluentValidation;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnit;

public class UpsertUnitCommand : IRequest<CommandResult>
{
    public UpsertUnitCommand(int? unitId, int unitTypeId, int scenicViewId, string unitNo, int floorId,
        double floorArea, decimal sellingPrice)
    {
        UnitId = unitId;
        UnitTypeId = unitTypeId;
        ScenicViewId = scenicViewId;
        UnitNo = unitNo;
        FloorId = floorId;
        FloorArea = floorArea;
        SellingPrice = sellingPrice;
    }

    public int? UnitId { get; }
    public int UnitTypeId { get; }
    public int ScenicViewId { get; }
    public string UnitNo { get; }
    public int FloorId { get; }
    public double FloorArea { get; }
    public decimal SellingPrice { get; }
}

public class UpsertUnitCommandValidator : AbstractValidator<UpsertUnitCommand>
{
    public UpsertUnitCommandValidator()
    {
        RuleFor(i => i.UnitTypeId).NotEmpty();
        RuleFor(i => i.ScenicViewId).NotEmpty();
        RuleFor(i => i.UnitNo).NotEmpty();
        RuleFor(i => i.FloorId).NotEmpty();
        RuleFor(i => i.FloorArea).NotEmpty();
        RuleFor(i => i.SellingPrice).NotEmpty();
    }
}