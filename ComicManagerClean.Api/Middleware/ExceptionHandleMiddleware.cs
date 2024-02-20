using ComicManagerClean.Contracts.Common;
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
        Log.Error(ex, "Error Happened!");

        // Return internal server error
        httpContext.Response.StatusCode = 500;
        await httpContext.Response.WriteAsJsonAsync(new TaskResult<string>()
        {
            Successful = false,
            ErrorList = new List<string>() { ex.StackTrace },
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