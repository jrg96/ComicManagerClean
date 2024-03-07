using ComicManagerClean.Domain.Shared.Enums;

namespace ComicManagerClean.Contracts.DTO.Character;

public class CharacterDto
{
    public Guid Id { get; set; }
    public string HeroName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public CharacterTypeEnum CharacterType { get; set; }
    public bool Deceased { get; set; }
}
