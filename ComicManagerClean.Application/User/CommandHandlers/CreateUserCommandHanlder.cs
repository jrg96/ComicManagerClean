using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.User.CommandHandlers;

public class CreateUserCommandHanlder : ICommandHandler<CreateUserCommand>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHanlder(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository, IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = await _userQueryRepository.GetUserByEmail(request.Email);

        // If user already exists, fail the command
        if (user != null)
        {
            return new CommandResult(false, new Error("CM-001", "User with that email already exists in database"));
        }

        await _userCommandRepository.Add(new Domain.Entities.User()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            LastName = request.LastName,
            Password = request.Password,
        });

        await _unitOfWork.SaveChangesAsync();
        return new CommandResult(true, Error.None);
    }
}
