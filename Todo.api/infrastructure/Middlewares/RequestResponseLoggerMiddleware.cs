// Copyright (C) TBC Bank. All Rights Reserved.

namespace Todo.api.infrastructure.Middlewares;

public class RequestResponseLoggerMiddleware : IMiddleware
{
    private readonly ILogger _logger;
    public RequestResponseLoggerMiddleware(ILogger<RequestResponseLoggerMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userIpAddress = context.Connection.RemoteIpAddress?.ToString();
        var queryString = context.Request.QueryString.ToString();
        var requestMethod = context.Request.Method;
        var requestPath = context.Request.Path;

        _logger.LogInformation("{@Start} {@Ip} {@QueryString} {@RequestMethod} {@RequestPath}",
            DateTime.UtcNow,
            userIpAddress,
            queryString,
            requestMethod,
            requestPath);

        await next(context).ConfigureAwait(false);

        _logger.LogInformation("{@End}", DateTime.UtcNow);
    }
}
