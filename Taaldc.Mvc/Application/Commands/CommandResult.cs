using Newtonsoft.Json;

namespace Taaldc.Mvc.Application.Commands;

public record CommandResult
{
    public CommandResult(string errorMessage, bool isSuccess)
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
    }

    public string ErrorMessage { get; private set; }
    public bool IsSuccess { get; private set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}