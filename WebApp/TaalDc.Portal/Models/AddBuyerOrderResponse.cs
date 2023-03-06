namespace TaalDc.Portal.Models;

public class AddBuyerOrderResponse
{
    public AddBuyerOrderResponse(string errorMessage, bool isSuccess, IDictionary<string, object> ret)
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
        Ret = ret;
    }

    public string ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }
    public IDictionary<string, object> Ret { get; set; }
}