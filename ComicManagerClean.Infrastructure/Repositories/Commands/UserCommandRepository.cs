using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Infrastructure.Context;
namespace ComicManagerClean.Infrastructure.Repositories.Commands;

public class UserCommandRepository : GenericCommandRepository<User>, IUserCommandRepository
{
    public UserCommandRepository(ComicManagerDbContext context) : base(context)
    {
    }
}
