using ComicManagerClean.Application.Abstractions;

namespace ComicManagerClean.Application.Comic.Commands;

public sealed record CreateComicCommand(
    string Name
    , DateTime ReleaseDate
    , int Chapters
) : ICommand;
