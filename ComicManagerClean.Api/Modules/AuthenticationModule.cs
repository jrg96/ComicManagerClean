using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Contracts.Authentication;
using ComicManagerClean.Contracts.Common;
using ComicManagerClean.Infrastructure.Constants;
using ComicManagerClean.Infrastructure.Services.Security.Contracts;
using MediatR;
using Microsoft.IdentityModel.Tokens;

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
            .Produces<TaskResult>(200)
            .Produces<TaskResult>(500)
            .Produces<TaskResult>(400);

        v1.MapPost("/login", Login)
            .Accepts<AuthenticationRequest>("application/json")
            .Produces<TaskResult<string>>(200)
            .Produces<TaskResult>(500)
            .Produces<TaskResult>(400);
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
            return TypedResults.BadRequest(new TaskResult()
            {
                Successful = false,
                ErrorList = new List<string>() { result.Error.Message }
            });
        }

        return TypedResults.Ok(new TaskResult()
        {
            ErrorList = new List<string>(),
            Successful = result.IsSuccess
        });
    }

    public async Task<IResult> Login(
        IConfiguration config
        , IJwtTokenService jwtTokenService
        , IMediator mediator
        , AuthenticationRequest request)
    {
        var result = await mediator.Send(new LoginUserCommand(
            request.Email,
            request.Password
        ));

        // If there was a failure, return Bad Request
        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(new TaskResult() {
                Successful = false,
                ErrorList = new List<string>() { result.Error.Message }
            });
        }

        // If everything is ok, return token data
        Dictionary<string, string> claims = new Dictionary<string, string>()
        {
            { JwtClaimConstants.USER_ID_CLAIM, result.Value.Id.ToString() },
            { JwtClaimConstants.USER_NAME_CLAIM, result.Value.Name },
            { JwtClaimConstants.USER_LAST_NAME_CLAIM, result.Value.LastName },
            { JwtClaimConstants.USER_ROLE_CLAIM, result.Value.Role.ToString() },
        };

        return TypedResults.Ok(new TaskResult<string>()
        {
            ErrorList = new List<string>(),
            Successful = true,
            Data = jwtTokenService.GetJwtToken(120, config["Jwt:Issuer"], config["Jwt:Key"]
                , SecurityAlgorithms.HmacSha256, claims)
        });
    }
}
