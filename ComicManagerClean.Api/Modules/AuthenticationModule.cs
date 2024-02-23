using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Contracts.Authentication;
using ComicManagerClean.Contracts.Common;
using MediatR;

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
            .Accepts<SignUpRequest>("application/json")
            .Produces<TaskResult<string>>(200)
            .Produces<TaskResult<string>>(500)
            .Produces<TaskResult<string>>(400);

        v1.MapPost("/login", Login)
            .Accepts<AuthenticationRequest>("application/json")
            .Produces<TaskResult<string>>(200)
            .Produces<TaskResult<string>>(500)
            .Produces<TaskResult<string>>(400);
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
    public async Task<IResult> Register(IMediator mediator, SignUpRequest request)
    {
        var result = await mediator.Send(new CreateUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        ));

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(new TaskResult<string>()
            {
                Successful = false,
                ErrorList = new List<string>() { result.Error.Message },
                Data = string.Empty
            });
        }

        return TypedResults.Ok(new TaskResult<string>()
        {
            ErrorList = new List<string>(),
            Successful = result.IsSuccess,
            Data = "User registered successfully!"
        });
    }

    public async Task<IResult> Login(IMediator mediator, AuthenticationRequest request)
    {
        var result = await mediator.Send(new LoginUserCommand(
            request.Email,
            request.Password
        ));

        // If there was a failure, return Bad Request
        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(new TaskResult<string>() {
                Successful = false,
                ErrorList = new List<string>() { result.Error.Message },
                Data = string.Empty
            });
        }

        return TypedResults.Ok(new TaskResult<string>()
        {
            ErrorList = new List<string>(),
            Successful = true,
            Data = "YOUR_JSON_WEB_TOKEN"
        });
    }
}
