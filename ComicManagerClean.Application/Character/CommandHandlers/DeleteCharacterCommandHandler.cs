using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Character.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.Character.CommandHandlers;

public class DeleteCharacterCommandHandler : ICommandHandler<DeleteCharacterCommand>
{
    private readonly ICharacterCommandRepository _characterCommandRepository;
    private readonly ICharacterQueryRepository _characterQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCharacterCommandHandler(ICharacterCommandRepository characterCommandRepository, ICharacterQueryRepository characterQueryRepository, IUnitOfWork unitOfWork)
    {
        _characterCommandRepository = characterCommandRepository;
        _characterQueryRepository = characterQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        // Check if character exists
        Domain.Entities.Character? character = await _characterQueryRepository.GetById(request.Id);

        if (character == null)
        {
            return new CommandResult(false, new Error("CM-03", "Character does not exist!"));
        }

        // Apply the delete operation
        await _characterCommandRepository.Remove(character);
        await _unitOfWork.SaveChangesAsync();

        return new CommandResult(true, Error.None);
    }
}
