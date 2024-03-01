using ComicManagerClean.Domain.Shared.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ComicManagerClean.Api.Middleware.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public RolesEnum Role { get; private set; }

    public RoleRequirement(RolesEnum role)
    {
        Role = role;
    }
}
