using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Common.QueryResponse;
using ComicManagerClean.Domain.Common.ValueObjects.Pagination;

namespace ComicManagerClean.Application.Character.Queries;

public sealed record GetCharactersByFilterQuery(
    PaginationCriteria criteria
) : ICommand<QueryResponse<Domain.Entities.Character>>;
