using ComicManagerClean.Application.Abstractions;

namespace ComicManagerClean.Application.Character.Commands;

public sealed record DeleteCharacterCommand(
    Guid Id
) : ICommand;
