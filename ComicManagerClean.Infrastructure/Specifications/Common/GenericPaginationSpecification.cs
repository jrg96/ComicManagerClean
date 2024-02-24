using Ardalis.Specification;
using ComicManagerClean.Domain.Common.ValueObjects.Pagination;
using System.Linq.Expressions;

namespace ComicManagerClean.Infrastructure.Specifications.Common;

public abstract class GenericPaginationSpecification<T> : Specification<T> where T : class
{
    protected ISpecificationBuilder<T> Builder { get; set; }
    protected Dictionary<string, Expression<Func<T, object?>>> OrderFunctions { get; set; }

    protected GenericPaginationSpecification(PaginationCriteria paginationCriteria, Dictionary<string, Expression<Func<T, object?>>> orderFunctions)
    {
        OrderFunctions = orderFunctions;

        // Apply Pagination
        Builder = Query
            .Skip((paginationCriteria.Page - 1) * paginationCriteria.Size)
            .Take(paginationCriteria.Size);
        
        // Apply sorting if applicable
        if (!string.IsNullOrEmpty(paginationCriteria.SortBy))
        {
            Builder = paginationCriteria.Ascending ?
                Builder.OrderBy(OrderFunctions[paginationCriteria.SortBy])
                : Builder.OrderByDescending(OrderFunctions[paginationCriteria.SortBy]);
        }
    }
}
