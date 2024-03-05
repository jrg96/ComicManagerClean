using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Infrastructure.Context;

namespace ComicManagerClean.Infrastructure.Repositories.Commands;

public class CharacterCommandRepository : GenericCommandRepository<Character>, ICharacterCommandRepository
{
    public CharacterCommandRepository(ComicManagerDbContext context) : base(context)
    {
    }
}
