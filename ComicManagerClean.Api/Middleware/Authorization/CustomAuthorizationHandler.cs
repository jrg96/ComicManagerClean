using Microsoft.AspNetCore.Authorization;

namespace ComicManagerClean.Api.Middleware.Authorization;

public class CustomAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        HttpContext httpContext = _httpContextAccessor.HttpContext;
        Domain.Entities.User user = (Domain.Entities.User)httpContext.Items["User"];

        // Verify if logged in user has the required permissions
        if (user != null && requirement.Role == user.Role)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail(new AuthorizationFailureReason(this, $" Logged in user is not {requirement.Role}"));
        return Task.CompletedTask;
    }
}
