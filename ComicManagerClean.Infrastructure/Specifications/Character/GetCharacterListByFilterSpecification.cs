using Ardalis.Specification;
using ComicManagerClean.Domain.Common.ValueObjects.Pagination;
using ComicManagerClean.Infrastructure.Specifications.Common;
using System.Linq.Expressions;

namespace ComicManagerClean.Infrastructure.Specifications.Character;

public class GetCharacterListByFilterSpecification : GenericPaginationSpecification<Domain.Entities.Character>
{
    private static readonly Dictionary<string, Expression<Func<Domain.Entities.Character, object?>>> _orderOptions = new Dictionary<string, Expression<Func<Domain.Entities.Character, object?>>>()
    {
        { "hero_name", character => character.HeroName },
        { "first_name", character => character.FirstName },
        { "last_name", character => character.LastName },
        { "date_birth", character => character.DateOfBirth},
        { "deceased", character => character.Deceased },
        { "character_type", character => character.CharacterType.ToString() },
    };

    public GetCharacterListByFilterSpecification(PaginationCriteria paginationCriteria, bool countScenario = false) : base(paginationCriteria, _orderOptions, countScenario)
    {
        if (!string.IsNullOrEmpty(paginationCriteria.SearchString))
        {
            Builder = Builder
                .Where(character => character.HeroName.Contains(paginationCriteria.SearchString.ToLower())
                || character.FirstName.Contains(paginationCriteria.SearchString.ToLower())
                || character.LastName.Contains(paginationCriteria.SearchString.ToLower()));
        }
    }
}
