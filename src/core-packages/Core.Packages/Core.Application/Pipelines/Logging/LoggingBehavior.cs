using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.Application.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly LoggerServiceBase _logger;

    public LoggingBehavior(IHttpContextAccessor contextAccessor, LoggerServiceBase logger)
    {
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> parameters = new()
        {
            new LogParameter{Type=request.GetType().Name, Value=request}
        };

        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = parameters,
            User = _contextAccessor.HttpContext.User?.Identity?.Name ?? "?",
        };

        _logger.Info(JsonSerializer.Serialize(logDetail));

        return await next();
    }
}
