﻿namespace Basket.API.Infrastructure.Exceptions;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}
