using System.Linq.Expressions;

namespace ComicManagerClean.Infrastructure.Specifications.Deprecated.Contracts;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    public Expression<Func<TEntity, bool>> Criteria { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();
    public Expression<Func<TEntity, object>> OrderBy { get; private set; }
    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
    public Expression<Func<TEntity, object>> GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;

    protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected BaseSpecification()
    {

    }

    protected virtual void AddInclude(Expression<Func<TEntity, object>> include)
    {
        Includes.Add(include);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }


}
