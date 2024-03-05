using ComicManagerClean.Application.User.Commands;
using Mapster;

namespace ComicManagerClean.Application.Common.Mappers;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserCommand, Domain.Entities.User>();
    }
}
