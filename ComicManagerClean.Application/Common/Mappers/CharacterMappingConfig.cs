using ComicManagerClean.Application.Character.Commands;
using Mapster;

namespace ComicManagerClean.Application.Common.Mappers;

public class CharacterMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCharacterCommand, Domain.Entities.Character>();
    }
}
