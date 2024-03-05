using Ardalis.Specification;
using ComicManagerClean.Domain.Entities;

namespace ComicManagerClean.Infrastructure.Specifications.Character;

public class GetCharacterByIdSpecification : Specification<Domain.Entities.Character>
{
    public GetCharacterByIdSpecification(Guid id)
    {
        Query
            .Where(character => character.Id == id);
    }
}
