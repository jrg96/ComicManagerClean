using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Character.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;
using Mapster;

namespace ComicManagerClean.Application.Character.CommandHandlers;

public class UpdateCharacterCommandHandler : ICommandHandler<UpdateCharacterCommand>
{
    private readonly ICharacterCommandRepository _characterCommandRepository;
    private readonly ICharacterQueryRepository _characterQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCharacterCommandHandler(ICharacterCommandRepository characterCommandRepository, ICharacterQueryRepository characterQueryRepository, IUnitOfWork unitOfWork)
    {
        _characterCommandRepository = characterCommandRepository;
        _characterQueryRepository = characterQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        // Check if character exists
        Domain.Entities.Character existingCharacter = await _characterQueryRepository.GetById(request.Id);

        if (existingCharacter == null)
        {
            return new CommandResult(false, new Error("CM-03", "Character does not exist!"));
        }

        request.Adapt(existingCharacter);
        await _characterCommandRepository.Update(existingCharacter);
        await _unitOfWork.SaveChangesAsync();

        return new CommandResult(true, Error.None);
    }
}
