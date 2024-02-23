
using System.Linq.Expressions;

namespace ComicManagerClean.Infrastructure.Specifications.Contracts;

public interface ISpecification<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<TEntity, object>> OrderBy { get; }
    Expression<Func<TEntity, object>> OrderByDescending { get; }
    Expression<Func<TEntity, object>> GroupBy { get; }

    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
