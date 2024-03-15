using ComicManagerClean.Application.Abstractions;

namespace ComicManagerClean.Application.Comic.Commands;

public sealed record DeleteComicCommand(
    Guid Id
) : ICommand;
