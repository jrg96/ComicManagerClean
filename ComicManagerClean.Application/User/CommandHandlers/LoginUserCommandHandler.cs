using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Services.Security;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.User.CommandHandlers;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Domain.Entities.User>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IPasswordSecurityService _passwordSecurityService;

    public LoginUserCommandHandler(IUserQueryRepository userQueryRepository, IPasswordSecurityService passwordSecurityService)
    {
        _userQueryRepository = userQueryRepository;
        _passwordSecurityService = passwordSecurityService;
    }

    public async Task<CommandResult<Domain.Entities.User>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // verify if user exists
        Domain.Entities.User user = await _userQueryRepository.GetUserByEmail(request.Email);
        
        if (user == null)
        {
            return new CommandResult<Domain.Entities.User>(null, false,
                new Error("CM-02", "User or password not valid"));
        }

        // verify if credentials are not valid
        if (!_passwordSecurityService.VerifyPassword(request.Password, user.Password, user.Salt))
        {
            return new CommandResult<Domain.Entities.User>(null, false,
                new Error("CM-02", "User or password not valid"));
        }

        // If evrything went successful, inform successful login
        return new CommandResult<Domain.Entities.User>(user, true, Error.None);
    }
}
