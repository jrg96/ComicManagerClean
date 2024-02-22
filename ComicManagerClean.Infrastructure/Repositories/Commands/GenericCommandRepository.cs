using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace ComicManagerClean.Infrastructure.Repositories.Commands;

public abstract class GenericCommandRepository<T> : IGenericCommandRepository<T> where T : class
{
    protected readonly ComicManagerDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericCommandRepository(ComicManagerDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task Update(T entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
    }
}
