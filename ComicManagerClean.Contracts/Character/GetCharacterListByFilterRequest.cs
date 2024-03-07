using ComicManagerClean.Domain.Common.ValueObjects.Pagination;

namespace ComicManagerClean.Contracts.Character;

public class GetCharacterListByFilterRequest
{
    public PaginationCriteria Pagination { get; set; }
}
