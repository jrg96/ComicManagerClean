using ComicManagerClean.Domain.Common.ValueObjects.Pagination;
using ComicManagerClean.Domain.Entities;

namespace ComicManagerClean.Domain.Repositories.Queries;

public interface ICharacterQueryRepository : IGenericQueryRepository<Character>
{
    Task<Character?> GetById(Guid id);
    Task<(int totalCount, IEnumerable<Character> result)> GetByFilter(PaginationCriteria criteria);
}
