using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Context;
using ComicManagerClean.Infrastructure.Specifications;
using ComicManagerClean.Infrastructure.Specifications.Contracts;

namespace ComicManagerClean.Infrastructure.Repositories.Queries;

public abstract class GenericQueryRepository<T> : IGenericQueryRepository<T> where T : class
{
    protected readonly ComicManagerDbContext _context;

    public GenericQueryRepository(ComicManagerDbContext context)
    {
        _context = context;
    }

    protected IEnumerable<T> Find(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }
}
