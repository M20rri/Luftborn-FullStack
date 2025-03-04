﻿namespace Luftborn.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = string.Empty;
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case Exceptions.ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _logger.LogError(JsonConvert.SerializeObject(validationException.Data));

                result = JsonConvert.SerializeObject(new
                {
                    message = "SystemError",
                    statusCode = validationException.ErrorCode,
                    data = validationException.Data
                });
                break;
        }
        return context.Response.WriteAsync(result);
    }
}
