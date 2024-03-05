using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Domain.Shared.Enums;

namespace ComicManagerClean.Application.Character.Commands;

public record CreateCharacterCommand(
    string HeroName,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    CharacterTypeEnum CharacterType,
    bool Deceased = false
) : ICommand;