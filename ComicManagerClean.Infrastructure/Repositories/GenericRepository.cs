using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ComicManagerDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ComicManagerDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
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
