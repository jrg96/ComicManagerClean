using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using MediatR;
using ComicManagerClean.Contracts.Character;
using ComicManagerClean.Application.Character.Commands;
using ComicManagerClean.Contracts.Common;

namespace ComicManagerClean.Api.Modules;

public class CharacterModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authGroup = CreateApiVersions(app);
        var v1 = authGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/", CreateCharacter)
            .RequireAuthorization("user_policy_requirement")
            .Accepts<CreateCharacterRequest>("application/json")
            .Produces<TaskResult>(200)
            .Produces<TaskResult>(400)
            .Produces<TaskResult>(500)
            .Produces<TaskResult>(403);
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

    /*
     * Endpoint functions
     */
    public async Task<IResult> CreateCharacter(IMediator mediator, CreateCharacterRequest request)
    {
        var result = await mediator.Send(new CreateCharacterCommand(
            request.HeroName
            , request.FirstName
            , request.LastName
            , request.DateOfBirth
            , request.CharacterType
            , request.Deceased
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
            Successful = true
        });
    }
}
