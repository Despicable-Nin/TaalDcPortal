using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

namespace Taaldc.Mvc.Application.Commands;

public record CommandResult
{
    private CommandResult( bool isSuccess,string errorMessage, int? id)
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
        Id = id;
    }

    private static CommandResult Create(bool isSuccess, string errorMessage, int? id) => new CommandResult( isSuccess, errorMessage, id);
    
    public static CommandResult Success(int? id) => CommandResult.Create(true, default, id);

    public static CommandResult Failed(int? id, string errorMessage) => CommandResult.Create(false, errorMessage, id);

    public string ErrorMessage { get; private set; }
    public bool IsSuccess { get; private set; }
    public int? Id { get; private set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
