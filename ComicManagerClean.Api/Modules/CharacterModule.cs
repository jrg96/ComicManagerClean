﻿using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using MediatR;
using ComicManagerClean.Contracts.Character;
using ComicManagerClean.Application.Character.Commands;
using ComicManagerClean.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using ComicManagerClean.Application.Character.Queries;
using Mapster;
using ComicManagerClean.Contracts.DTO.Character;

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

        v1.MapPut("/", UpdateCharacter)
            .RequireAuthorization("user_policy_requirement")
            .Accepts<UpdateCharacterRequest>("application/json")
            .Produces<TaskResult>(200)
            .Produces<TaskResult>(400)
            .Produces<TaskResult>(500)
            .Produces<TaskResult>(403);

        v1.MapDelete("/{characterId:guid}", DeleteCharacter)
            .RequireAuthorization("user_policy_requirement")
            .Produces<TaskResult>(200)
            .Produces<TaskResult>(400)
            .Produces<TaskResult>(500)
            .Produces<TaskResult>(403);

        v1.MapPost("/list", GetCharacterListByFilter)
            .RequireAuthorization("user_policy_requirement")
            .Produces<TaskResult<GetCharacterListByFilterResponse>>(200)
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

    public async Task<IResult> UpdateCharacter(IMediator mediator, UpdateCharacterRequest request)
    {
        var result = await mediator.Send(new UpdateCharacterCommand(
            request.Id
            , request.HeroName
            , request.FirstName
            , request .LastName
            , request .DateOfBirth
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
            Successful = true,
            ErrorList = new List<string>()
        });
    }

    public async Task<IResult> DeleteCharacter(IMediator mediator, [FromRoute]  Guid characterId)
    {
        var result = await mediator.Send(new DeleteCharacterCommand(characterId));

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
            ErrorList = new List<string>()
        });
    }

    public async Task<IResult> GetCharacterListByFilter(IMediator mediator, GetCharacterListByFilterRequest request)
    {
        var result = await mediator.Send(new GetCharactersByFilterQuery(request.Pagination));

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(new TaskResult() 
            {
                Successful = false,
                ErrorList = new List<string>() { result.Error.Message }
            });
        }

        return TypedResults.Ok(new TaskResult<GetCharacterListByFilterResponse>()
        {
            Successful = true,
            ErrorList = new List<string>(),
            Data = new GetCharacterListByFilterResponse()
            {
                TotalCount = result.Value.TotalCount,
                Data = result.Value.Data
                    .Select(character => character.Adapt<CharacterDto>())
                    .ToList()
            }
        });
    }
}
