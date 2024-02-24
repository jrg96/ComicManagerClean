using Ardalis.Specification;

namespace ComicManagerClean.Infrastructure.Specifications.User;

public class GetUserByEmailSpecification : Specification<Domain.Entities.User>
{
    public GetUserByEmailSpecification(string email)
    {
        Query.Where(user => user.Email == email);
    }
}
