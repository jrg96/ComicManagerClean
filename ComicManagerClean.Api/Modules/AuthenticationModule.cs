using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Contracts.Authentication;
using ComicManagerClean.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            .Produces<TaskResult<string>>(400)
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
}
