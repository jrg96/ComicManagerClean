using ComicManagerClean.Contracts.Common;
using FluentValidation;
using Serilog;

namespace ComicManagerClean.Api.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch(Exception e)
        {
            await HandleException(e, httpContext);
        }
    }

    private async Task HandleException(Exception ex, HttpContext httpContext)
    {
        List<string> errors = new List<string>();
        Log.Error(ex, "Error Happened!");

        // For Validation errors regarding Command/Query params
        // return a bad request
        if (ex is ValidationException)
        {
            httpContext.Response.StatusCode = 400;
            ValidationException validationException = (ValidationException)ex;

            validationException.Errors
                .ToList()
                .ForEach(error => errors.Add($"{error.PropertyName} {error.ErrorMessage}"));
        }
        else
        {
            httpContext.Response.StatusCode = 500;
            errors.Add($"{ex.Message} {ex.StackTrace}");
        }

        await httpContext.Response.WriteAsJsonAsync(new TaskResult<string>()
        {
            Successful = false,
            ErrorList = errors,
            Data = string.Empty
        }) ;
    }
}

// Extension method for middleware exception handler
public static class ExceptionHandleMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandleMiddleware>();
    }
}