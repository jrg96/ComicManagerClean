using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Comic.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Shared;
using Mapster;

namespace ComicManagerClean.Application.Comic.CommandHandlers;

public class CreateComicCommandHandler : ICommandHandler<CreateComicCommand>
{
    private readonly IComicCommandRepository _comicCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateComicCommandHandler(IComicCommandRepository comicCommandRepository, IUnitOfWork unitOfWork)
    {
        _comicCommandRepository = comicCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(CreateComicCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Comic comic = request.Adapt<Domain.Entities.Comic>();
        comic.Id = Guid.NewGuid();

        await _comicCommandRepository.Add(comic);
        await _unitOfWork.SaveChangesAsync();

        return new CommandResult(true, Error.None);
    }
}
