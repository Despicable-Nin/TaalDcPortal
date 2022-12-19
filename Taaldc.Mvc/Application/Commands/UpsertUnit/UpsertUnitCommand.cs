using FluentValidation;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnit;

public class UpsertUnitCommand : IRequest<CommandResult>
{
    public UpsertUnitCommand(int? unitId, int unitTypeId, int scenicViewId, string unitNo, int floorId, double floorArea, decimal sellingPrice)
    {
        UnitId = unitId;
        UnitTypeId = unitTypeId;
        ScenicViewId = scenicViewId;
        UnitNo = unitNo;
        FloorId = floorId;
        FloorArea = floorArea;
        SellingPrice = sellingPrice;
    }
    
    public int? UnitId { get; private set; }
    public int UnitTypeId { get; private set; }
    public int ScenicViewId { get; private set; }
    public string UnitNo { get; private set; }
    public int FloorId { get; private set; }
    public double FloorArea { get; private set; }
    public decimal SellingPrice { get; private set; }
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