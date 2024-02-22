using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Infrastructure.Context;

namespace ComicManagerClean.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ComicManagerDbContext context) : base(context)
    {
    }
}
