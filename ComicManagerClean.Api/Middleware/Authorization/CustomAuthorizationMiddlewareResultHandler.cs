using ComicManagerClean.Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;

namespace ComicManagerClean.Api.Middleware.Authorization;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        // Hanlde failures when RoleRequirement is used
        if (authorizeResult.Challenged && !authorizeResult.Succeeded && policy.Requirements.Any(req => req is RoleRequirement))
        {
            TaskResult result = new TaskResult()
            {
                Successful = false,
                ErrorList = new List<string>()
                {
                    { "Unauthorized" }
                }
            };

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(result);
            return;
        }

        // Fallback to the default implementation.
        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}

public static class CustomAuthorizationMiddlewareResultHandlerExtensions
{
    public static IServiceCollection AddCustomAuthMidldlewareResultHandler(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

        return services;
    }
}
