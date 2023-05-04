using FluentValidation;
using MediatR;

namespace Taaldc.Catalog.API.Application.Commands.UpsertUnit;

public class UpsertUnitCommand : IRequest<CommandResult>
{
    public UpsertUnitCommand(
        int? unitId,
        int? unitStatusId,
        int unitTypeId,
        int scenicViewId,
        string unitNo,
        int floorId,
        double floorArea,
        double balconyArea,
        decimal sellingPrice,
        string tower,
        string remarks,
        bool isActive)
    {
        UnitId = unitId;
        UnitStatusId = unitStatusId;
        UnitTypeId = unitTypeId;
        ScenicViewId = scenicViewId;
        UnitNo = unitNo;
        FloorId = floorId;
        FloorArea = floorArea;
        BalconyArea = balconyArea;
        SellingPrice = sellingPrice;
        Tower = tower;
        Remarks = !string.IsNullOrEmpty(remarks) ? remarks : "";
        IsActive = isActive;
    }

    public int? UnitId { get; }
    public int UnitTypeId { get; }
    public int ScenicViewId { get; }
    public string UnitNo { get; }
    public int FloorId { get; }
    public double FloorArea { get; }
    public double BalconyArea { get; }
    public decimal SellingPrice { get; }
    public string Tower { get; }
    public string Remarks { get; }
    public int? UnitStatusId { get; }
    public bool IsActive { get; }
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
        RuleFor(i => i.BalconyArea).NotEmpty();
        RuleFor(i => i.SellingPrice).NotEmpty();
        RuleFor(i => i.Tower).NotEmpty();
        RuleFor(i => i.UnitStatusId).NotEmpty().When(i => i.UnitId.HasValue);
    }
}