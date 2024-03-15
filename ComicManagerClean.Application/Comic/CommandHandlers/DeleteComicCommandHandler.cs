using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Comic.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;

namespace ComicManagerClean.Application.Comic.CommandHandlers;

public class DeleteComicCommandHandler : ICommandHandler<DeleteComicCommand>
{
    private readonly IComicQueryRepository _comicQueryRepository;
    private readonly IComicCommandRepository _comicCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteComicCommandHandler(IUnitOfWork unitOfWork, IComicQueryRepository comicQueryRepository, IComicCommandRepository comicCommandRepository)
    {
        _comicCommandRepository = comicCommandRepository;
        _comicQueryRepository = comicQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(DeleteComicCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Comic? comic = await _comicQueryRepository.GetById(request.Id);

        if (comic == null)
        {
            return new CommandResult(false, new Error("CM-04", $"Comic with Id { request.Id } does not exist!"));
        }

        await _comicCommandRepository.Remove(comic);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CommandResult(true, Error.None);
    }
}
