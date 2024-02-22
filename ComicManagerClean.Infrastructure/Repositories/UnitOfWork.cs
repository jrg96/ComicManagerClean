using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Infrastructure.Context;

namespace ComicManagerClean.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ComicManagerDbContext _context;

    public UnitOfWork(ComicManagerDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
