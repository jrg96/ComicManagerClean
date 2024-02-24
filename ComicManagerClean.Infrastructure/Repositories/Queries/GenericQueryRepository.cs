﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Context;

namespace ComicManagerClean.Infrastructure.Repositories.Queries;

public abstract class GenericQueryRepository<T> : IGenericQueryRepository<T> where T : class
{
    protected readonly ComicManagerDbContext _context;

    public GenericQueryRepository(ComicManagerDbContext context)
    {
        _context = context;
    }

    protected IQueryable<T> Find(ISpecification<T> specification)
    {
        return SpecificationEvaluator.Default.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }
}
