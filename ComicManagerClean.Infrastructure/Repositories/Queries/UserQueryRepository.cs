using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Repositories.Queries;

public class UserQueryRepository : GenericQueryRepository<User>, IUserQueryRepository
{
    public UserQueryRepository(ComicManagerDbContext context) : base(context)
    {
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync();
    }
}
