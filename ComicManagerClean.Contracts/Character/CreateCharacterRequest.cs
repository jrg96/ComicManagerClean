using ComicManagerClean.Domain.Shared.Enums;

namespace ComicManagerClean.Contracts.Character;

public class CreateCharacterRequest
{
    public string HeroName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public CharacterTypeEnum CharacterType { get; set; }
    public bool Deceased { get; set; }
}
