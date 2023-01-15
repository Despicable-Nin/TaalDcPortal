namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public record SellUnitCommandResult 
{
    private SellUnitCommandResult(bool isSuccess, string errorMessage, IDictionary<string,object> ret) 
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
        Ret = ret;
    }

    public string ErrorMessage { get; }
    public bool IsSuccess { get; }
    public  IDictionary<string,object> Ret { get; }

    public static SellUnitCommandResult Create(bool isSuccess, string errorMessage, IDictionary<string, object> ret)
    {
        return new SellUnitCommandResult(isSuccess, errorMessage, ret);
    }
}