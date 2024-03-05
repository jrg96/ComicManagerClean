using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Infrastructure.Context;
using ComicManagerClean.Infrastructure.Specifications.Character;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Repositories.Queries;

public class CharacterQueryRepository : GenericQueryRepository<Character>, ICharacterQueryRepository
{
    public CharacterQueryRepository(ComicManagerDbContext context) : base(context)
    {
    }

    public async Task<Character?> GetById(Guid id)
    {
        return await Find(new GetCharacterByIdSpecification(id))
            .FirstOrDefaultAsync();
    }
}
