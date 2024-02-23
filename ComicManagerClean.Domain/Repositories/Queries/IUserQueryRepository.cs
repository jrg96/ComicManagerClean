using ComicManagerClean.Domain.Entities;
namespace ComicManagerClean.Domain.Repositories.Queries;

public interface IUserQueryRepository : IGenericQueryRepository<User>
{
    Task<User?> GetUserByEmail(string email);
}
