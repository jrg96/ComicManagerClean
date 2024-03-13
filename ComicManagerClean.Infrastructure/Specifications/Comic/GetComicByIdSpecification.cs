using Ardalis.Specification;

namespace ComicManagerClean.Infrastructure.Specifications.Comic;

public class GetComicByIdSpecification : Specification<Domain.Entities.Comic>
{
    public GetComicByIdSpecification(Guid id)
    {
        Query
            .Where(comic => comic.Id == id);
    }
}
