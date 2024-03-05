using ComicManagerClean.Domain.Shared.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ComicManagerClean.Api.Middleware.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public RolesEnum[] Roles { get; private set; }

    public RoleRequirement(params RolesEnum[] roles)
    {
        Roles = roles;
    }
}
