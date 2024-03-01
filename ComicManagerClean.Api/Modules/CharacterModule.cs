using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using ComicManagerClean.Api.Attributes;
using ComicManagerClean.Domain.Shared.Enums;

namespace ComicManagerClean.Api.Modules;

public class CharacterModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authGroup = CreateApiVersions(app);
        var v1 = authGroup.MapGroup("").HasApiVersion(1);

        v1.MapGet("/testadmin", () =>
        {
            return "You're an admin";
        })
        .RequireAuthorization("admin_policy_requirement");
    }

    private RouteGroupBuilder CreateApiVersions(IEndpointRouteBuilder app)
    {
        ApiVersionSet versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        return app
            .MapGroup("/api/v{version:apiVersion}/character")
            .WithApiVersionSet(versionSet);
    }
}
