using Microsoft.AspNetCore.Mvc;
using Todo.application.Exceptions;
using Todo.application.Exceptions.Abstractions;

namespace Todo.api.infrastructure.CustomGlobalErrorHandler;

public class GlobalErrorHandler : ProblemDetails
{
    public GlobalErrorHandler(HttpContext context, Exception ex)
    {
        Extensions["TraceId"] = context.TraceIdentifier;
        Instance = context.Request.Path;
        Title = "Something went wrong";
        if (ex is ICustomException)
            HandleCustomException((dynamic)ex);
        else HandleException();
    }

    private void HandleException()
    {
        Status = StatusCodes.Status500InternalServerError;
        Type = @"https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
        Detail = "Something went wrong on the server";
    }

    private void HandleCustomException(Exception ex)
    {
        Detail = ex.Message;
        if (ex is IBadRequest)
            HandleBadRequests();
        else if (ex is INotFound)
            HandleNotFounds();
        else if (ex is IConflict)
            HandleConflicts();

    }

    private void HandleBadRequests()
    {
        Status = StatusCodes.Status400BadRequest;
        Type = @"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
    }

    private void HandleNotFounds()
    {
        Status = StatusCodes.Status404NotFound;
        Type = @"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
    }

    private void HandleConflicts()
    {
        Status = StatusCodes.Status409Conflict;
        Type = @"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
    }
}
