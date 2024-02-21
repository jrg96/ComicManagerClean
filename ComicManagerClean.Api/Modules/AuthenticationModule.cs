using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using ComicManagerClean.Contracts.Authentication;
using ComicManagerClean.Contracts.Common;

namespace ComicManagerClean.Api.Modules;

public class AuthenticationModule : CarterModule
{
    public AuthenticationModule()
    {
        
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authGroup = CreateApiVersions(app);
        var v1 = authGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/register", Register)
            .Produces<TaskResult<string>>(200)
            .Produces<TaskResult<string>>(500)
            .Accepts<SignUpRequest>("application/json");
    }

    private RouteGroupBuilder CreateApiVersions(IEndpointRouteBuilder app)
    {
        ApiVersionSet versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        return app
            .MapGroup("/api/v{version:apiVersion}/auth")
            .WithApiVersionSet(versionSet);
    }


    /*
     * Endpoint functions
     */
    public async Task<TaskResult<string>> Register(SignUpRequest request)
    {
        return new TaskResult<string>()
        {
            ErrorList = new List<string>(),
            Successful = true,
            Data = "Hello World!"
        };
    }
}
