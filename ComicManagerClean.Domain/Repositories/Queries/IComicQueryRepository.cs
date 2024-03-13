using ComicManagerClean.Domain.Entities;

namespace ComicManagerClean.Domain.Repositories.Queries;

public interface IComicQueryRepository : IGenericQueryRepository<Comic>
{
    Task<Comic?> GetById(Guid id);
}
