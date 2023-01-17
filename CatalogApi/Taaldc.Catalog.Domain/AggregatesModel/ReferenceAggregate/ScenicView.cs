using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

public sealed class ScenicView : Enumeration, IAggregateRoot
{
    public static IEnumerable<ScenicView> ViewOptions = new[]
    {
        new ScenicView(1, "Highlands View"),
        new ScenicView(2, "Rotonda View"),
        new ScenicView(3, "Taal View"),
        new ScenicView(4, "Manila Skyline")
    };

    public ScenicView(int id, string name) : base(id, name)
    {
    }
}