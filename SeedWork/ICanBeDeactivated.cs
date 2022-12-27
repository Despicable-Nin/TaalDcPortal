namespace SeedWork;

public interface ICanBeDeactivated
{
    public bool IsActive { get; }
    void Deactivate();
}
