using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Character.Queries;
using ComicManagerClean.Application.Common.QueryResponse;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.Character.QueryHandlers;

public class GetCharactersByFilterQueryHandler : ICommandHandler<GetCharactersByFilterQuery, QueryResponse<Domain.Entities.Character>>
{
    private readonly ICharacterQueryRepository _characterQueryRepository;

    public GetCharactersByFilterQueryHandler(ICharacterQueryRepository characterQueryRepository)
    {
        _characterQueryRepository = characterQueryRepository;
    }

    public async Task<CommandResult<QueryResponse<Domain.Entities.Character>>> Handle(GetCharactersByFilterQuery request, CancellationToken cancellationToken)
    {
        (int count, IEnumerable<Domain.Entities.Character> data) = await _characterQueryRepository.GetByFilter(request.criteria);
        QueryResponse<Domain.Entities.Character> result = new QueryResponse<Domain.Entities.Character>() 
        {
            Data = data,
            TotalCount = count
        };


        return new CommandResult<QueryResponse<Domain.Entities.Character>>(result, true, Error.None);
    }
}
