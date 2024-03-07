using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Context;

namespace ComicManagerClean.Infrastructure.Repositories.Queries;

public class ComicQueryRepository : GenericQueryRepository<Comic>, IComicQueryRepository
{
    public ComicQueryRepository(ComicManagerDbContext context) : base(context)
    {
    }
}
