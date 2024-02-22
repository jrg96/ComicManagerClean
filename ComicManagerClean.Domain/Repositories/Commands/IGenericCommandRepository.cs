namespace ComicManagerClean.Domain.Repositories.Commands;

public interface IGenericCommandRepository<T> where T : class
{
    Task Add(T entity);
    Task Remove(T entity);
    Task Update(T entity);
}
