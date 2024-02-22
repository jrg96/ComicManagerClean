using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.User.CommandHandlers;

public class CreateUserCommandHanlder : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHanlder(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Add(new Domain.Entities.User()
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
