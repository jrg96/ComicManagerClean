using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using MediatR;
using ComicManagerClean.Application.Comic.Commands;
using ComicManagerClean.Contracts.Comic;
using ComicManagerClean.Contracts.Common;

namespace ComicManagerClean.Api.Modules;

public class ComicModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authGroup = CreateApiVersions(app);
        var v1 = authGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/", CreateComic)
            .RequireAuthorization("user_policy_requirement")
            .Accepts<CreateComicRequest>("application/json")
            .Produces<TaskResult>(200)
            .Produces<TaskResult>(400)
            .Produces<TaskResult>(500)
            .Produces<TaskResult>(403);

        v1.MapPut("/", UpdateComic)
            .RequireAuthorization("user_policy_requirement")
            .Accepts<UpdateComicRequest>("application/json")
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
            .MapGroup("/api/v{version:apiVersion}/comic")
            .WithApiVersionSet(versionSet);
    }

    /*
     * Endpoint functions
     */
    public async Task<IResult> CreateComic(IMediator mediator, CreateComicRequest request)
    {
        var result = await mediator.Send(new CreateComicCommand(
            request.Name,
            request.ReleaseDate,
            request.Chapters
        ));

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(new TaskResult()
            {
                Successful = false,
                ErrorList = new List<string>() { result.Error.Message }
            });
        }

        return TypedResults.Ok(
            new TaskResult()
            {
                Successful = true,
                ErrorList = new List<string>() { }
            }
        );
    }

    public async Task<IResult> UpdateComic(IMediator mediator, UpdateComicRequest request)
    {
        var result = await mediator.Send(new UpdateComicCommand(
            request.Id,
            request.Name,
            request.ReleaseDate,
            request.Chapters
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
            Successful = true,
            ErrorList = new List<string>() { }
        });
    }
}
