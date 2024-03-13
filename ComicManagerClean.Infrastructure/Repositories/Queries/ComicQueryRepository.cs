using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Context;
using ComicManagerClean.Infrastructure.Specifications.Comic;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Repositories.Queries;

public class ComicQueryRepository : GenericQueryRepository<Comic>, IComicQueryRepository
{
    public ComicQueryRepository(ComicManagerDbContext context) : base(context)
    {
    }

    public async Task<Comic?> GetById(Guid id)
    {
        return await Find(new GetComicByIdSpecification(id))
            .FirstOrDefaultAsync();
    }
}
