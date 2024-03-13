using ComicManagerClean.Application.Abstractions;

namespace ComicManagerClean.Application.Comic.Commands;

public sealed record UpdateComicCommand(
    Guid Id,
    string Name,
    DateTime ReleaseDate,
    int Chapters
) : ICommand;
