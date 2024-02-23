using ComicManagerClean.Infrastructure.Specifications.Contracts;

namespace ComicManagerClean.Infrastructure.Specifications.User;

public class GetUserByEmailSpecification : BaseSpecification<Domain.Entities.User>, ISpecification<Domain.Entities.User>
{
    public GetUserByEmailSpecification(string email) : base(user => user.Email == email)
    {
        
    }
}
