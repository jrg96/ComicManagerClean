using ComicManagerClean.Contracts.Common;
using ComicManagerClean.Domain.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace ComicManagerClean.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private IList<RolesEnum> _roles;

    public CustomAuthorizeAttribute(params RolesEnum[] roles)
    {
        _roles = roles ?? new RolesEnum[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // If method we're analyzing has AllowAnonymous attached, skip authorization
        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
        {
            return;
        }

        Domain.Entities.User user = (Domain.Entities.User)context.HttpContext.Items["User"];

        // If user's role is not in the list of authorized users for the endpoint, return unauthorized
        if (user == null || (user != null && !_roles.Contains(user.Role)))
        {
            TaskResult<string> result = new TaskResult<string>()
            { 
                Successful = false,
                ErrorList = new List<string>()
                {
                    { "Unauthorized" }
                },
                Data = string.Empty,
            };

            context.Result = new JsonResult(result) 
            { 
                StatusCode = StatusCodes.Status401Unauthorized 
            };
        }
    }
}
