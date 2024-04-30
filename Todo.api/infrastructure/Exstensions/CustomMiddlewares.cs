// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.api.infrastructure.Middlewares;

namespace Todo.api.infrastructure.Exstensions;

public static class CustomMiddlewares
{
    public static void AddGlobalErrorHandling(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
    }

    public static void AddCustomRequestResponseLogging(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<RequestResponseLoggerMiddleware>();
    }
}
