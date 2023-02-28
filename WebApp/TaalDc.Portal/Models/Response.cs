﻿namespace TaalDc.Portal.Models;

public class Response
{
    public Response(string errorMessage, bool isSuccess, int? id)
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
        Id = id;
    }

    public string ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }
    public int? Id { get; set; }
}