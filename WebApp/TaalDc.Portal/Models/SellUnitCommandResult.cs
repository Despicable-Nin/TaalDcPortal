﻿namespace TaalDc.Portal.Models;

public class SellUnitCommandResult
{
    public string ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }
    public IDictionary<string, object> Ret { get; set; }
}