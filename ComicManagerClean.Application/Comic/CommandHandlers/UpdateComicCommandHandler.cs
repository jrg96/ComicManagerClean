using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Comic.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;
using Mapster;

namespace ComicManagerClean.Application.Comic.CommandHandlers;

public class UpdateComicCommandHandler : ICommandHandler<UpdateComicCommand>
{
    private readonly IComicQueryRepository _comicQueryRepository;
    private readonly IComicCommandRepository _comicCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateComicCommandHandler(IComicQueryRepository comicQueryRepository, IComicCommandRepository comicCommandRepository, IUnitOfWork unitOfWork)
    {
        _comicQueryRepository = comicQueryRepository;
        _comicCommandRepository = comicCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult> Handle(UpdateComicCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Comic? comic = await _comicQueryRepository.GetById(request.Id);

        if (comic == null)
        {
            return new CommandResult(false, new Error("CM-04", $"Comic with id { request.Id } not found!"));
        }

        request.Adapt(comic);
        await _comicCommandRepository.Update(comic);
        await _unitOfWork.SaveChangesAsync();

        return new CommandResult(true, Error.None);
    }
}
