using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Character.Commands;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.Character.CommandHandlers;

public class CreateCharacterCommandHandler : ICommandHandler<CreateCharacterCommand>
{
    private readonly ICharacterCommandRepository _characterCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCharacterCommandHandler(ICharacterCommandRepository characterCommandRepository, IUnitOfWork unitOfWork)
    {
        _characterCommandRepository = characterCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        // create new Character based on the input params
        Domain.Entities.Character character = new Domain.Entities.Character()
        {
            Id = new Guid(),
            HeroName = request.HeroName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            CharacterType = request.CharacterType,
            Deceased = request.Deceased
        };

        await _characterCommandRepository.Add(character);
        await _unitOfWork.SaveChangesAsync();

        return new CommandResult(true, Error.None);
    }
}
