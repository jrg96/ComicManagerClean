using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Infrastructure.Context;

namespace ComicManagerClean.Infrastructure.Repositories.Commands;

public class ComicCommandRepository : GenericCommandRepository<Comic>, IComicCommandRepository
{
    public ComicCommandRepository(ComicManagerDbContext context) : base(context)
    {
    }
}
