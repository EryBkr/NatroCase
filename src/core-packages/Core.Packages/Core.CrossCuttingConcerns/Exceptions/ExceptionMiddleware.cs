using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly LoggerServiceBase _logger;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, LoggerServiceBase logger)
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler();
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> parameters = new()
        {
            new LogParameter{Type=context.GetType().Name, Value=exception.ToString()}
        };

        LogDetailWithException logDetail = new()
        {
            MethodName = _next.Method.Name,
            Parameters = parameters,
            User = _contextAccessor.HttpContext?.User?.Identity?.Name ?? "?",
            ExceptionMessage = exception.Message
        };

        _logger.Error(JsonSerializer.Serialize(logDetail));

        return Task.CompletedTask;
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}
