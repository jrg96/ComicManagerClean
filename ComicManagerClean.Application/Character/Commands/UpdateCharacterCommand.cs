using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Domain.Shared.Enums;

namespace ComicManagerClean.Application.Character.Commands;

public sealed record UpdateCharacterCommand(
    Guid Id,
    string HeroName,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    CharacterTypeEnum CharacterType,
    bool Deceased
) : ICommand;