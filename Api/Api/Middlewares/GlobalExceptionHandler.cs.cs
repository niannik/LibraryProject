using Api.Models;
using Application.Common;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An exception occured. Details: {ExceptionMessage}", exception.Message);

        var type = exception.GetType();
        if (type == typeof(AppValidationException))
        {
            await HandleValidationExceptionAsync(httpContext, (exception as AppValidationException)!);
            return true;
        }

        await HandleGeneralExceptionAsync(httpContext, exception);

        return true;
    }

    private Task HandleValidationExceptionAsync(HttpContext httpContext, AppValidationException exception)
    {
        _logger.LogError(exception, "An exception occured. Details: {Errors}", exception.Errors);
        var responseBody = new BadRequestBody(exception.Errors);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return httpContext.Response.WriteAsJsonAsync(responseBody);
    }

    private Task HandleGeneralExceptionAsync(HttpContext httpContext, Exception exception)
    {
        const string productionMessage = "سرور به مشکل برخورده است";

        string message = _hostEnvironment.IsProduction() ? productionMessage : exception.Message;
        var responseBody = new InternalServerErrorBody(new Error(message, "InternalServerError"));

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return httpContext.Response.WriteAsJsonAsync(responseBody);
    }
}
