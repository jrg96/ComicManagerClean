namespace ComicManagerClean.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task Add(T entity);
    Task Remove(T entity);
    Task Update(T entity);
}
