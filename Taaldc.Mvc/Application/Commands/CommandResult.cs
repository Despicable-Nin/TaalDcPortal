using Newtonsoft.Json;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Mvc.Application.Commands;

public record CommandResult
{
    private CommandResult(bool isSuccess, string errorMessage, int? id)
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
        Id = id;
    }

    public string ErrorMessage { get; }
    public bool IsSuccess { get; }
    public int? Id { get; }

    private static CommandResult Create(bool isSuccess, string errorMessage, int? id)
    {
        return new(isSuccess, errorMessage, id);
    }

    public static CommandResult Success(int? id)
    {
        return Create(true, default, id);
    }

    public static CommandResult Failed(int? id, string errorMessage)
    {
        return Create(false, errorMessage, id);
    }

    public static CommandResult Failed(int? id, Type type)
    {
        return Failed(id, $"{type} with {id} not found.");
    }

    public static CommandResult EntityNotFoundResult(Entity entity)
    {
        return Failed(entity.Id, entity.GetType());
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
