using CommonLibrary.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Packages.Loggings;
using Packages.Loggings.SeriLog;
using System.Text.Json;

namespace Packages.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerServiceBase;

    public LoggingBehavior(IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServiceBase)
    {
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> logParameters =
            new()
            {
                new LogParameter{Type= request.GetType().Name, Value= request },
            };

        LogDetail logDetail
            = new()
            {
                MethodName = next.Method.Name,
                Parameters = logParameters,
                User = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(p => p.Type == Utility.JwtClaims.UserName)?.Value ?? Utility.JwtClaims.Unknown
            };

        _loggerServiceBase.Info(JsonSerializer.Serialize(logDetail));
        return await next();
    }
}
