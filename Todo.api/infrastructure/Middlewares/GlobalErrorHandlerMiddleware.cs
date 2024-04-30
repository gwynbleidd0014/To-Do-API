// Copyright (C) TBC Bank. All Rights Reserved.


using Todo.api.infrastructure.CustomGlobalErrorHandler;

namespace Todo.api.infrastructure.Middlewares;

public class GlobalErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
    public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
    {

        _logger = logger;

    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var problem = new GlobalErrorHandler(context, ex);
            _logger.LogError(ex,"Exception");
            await context.Response.WriteAsJsonAsync(problem).ConfigureAwait(false);
        }
    }
}
