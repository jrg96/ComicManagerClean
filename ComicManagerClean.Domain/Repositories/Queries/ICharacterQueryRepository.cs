using ComicManagerClean.Domain.Entities;

namespace ComicManagerClean.Domain.Repositories.Queries;

public interface ICharacterQueryRepository : IGenericQueryRepository<Character>
{
    Task<Character?> GetById(Guid id);
}
