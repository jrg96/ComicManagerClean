using ComicManagerClean.Domain.Shared.Enums;
namespace ComicManagerClean.Domain.Entities;

public class Character
{
    public Guid Id { get; set; }
    public string HeroName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public CharacterTypeEnum CharacterType { get; set; }
    public bool Deceased { get; set; }

    // Navigation properties
    public IList<CharacterComic> CharacterComics { get; set; }

}
